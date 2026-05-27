// HomeGrown — Interactive Farm Map
// Requires Leaflet.js to be loaded before this script.

(function () {
    'use strict';

    var DEFAULT_CENTER = [38.2919, -122.4580];
    var DEFAULT_ZOOM = 12;

    var map;
    var allFarms = [];
    var markerMap = {}; // farmId -> { marker, farm }
    var activeCategory = 'All';
    var searchQuery = '';

    // ─── Category emoji map ───────────────────────────────────────────────────
    var CATEGORY_EMOJI = {
        'Produce': '🥬',
        'Dairy & Eggs': '🥚',
        'Bread & Baked Goods': '🍞',
        'Honey & Preserves': '🍯',
        'Meat & Poultry': '🥩',
        'Wine & Beverages': '🍷'
    };

    var CATEGORY_COLOR = {
        'Produce': '#4a8c3f',
        'Dairy & Eggs': '#d4a017',
        'Bread & Baked Goods': '#c4622d',
        'Honey & Preserves': '#e08b2f',
        'Meat & Poultry': '#8b3a3a',
        'Wine & Beverages': '#6b2f8b'
    };

    // ─── Init ─────────────────────────────────────────────────────────────────
    document.addEventListener('DOMContentLoaded', function () {
        initMap();
        loadFarms();
        bindFilterChips();
        bindSearch();
        bindRecenter();
    });

    function initMap() {
        map = L.map('farmMap', {
            center: DEFAULT_CENTER,
            zoom: DEFAULT_ZOOM,
            zoomControl: false,
            scrollWheelZoom: true
        });

        // OpenStreetMap tiles
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
            maxZoom: 19
        }).addTo(map);

        // Custom zoom controls (top-left)
        L.control.zoom({ position: 'topleft' }).addTo(map);
    }

    // ─── Load farms from API ──────────────────────────────────────────────────
    function loadFarms() {
        fetch('/api/farms')
            .then(function (r) { return r.json(); })
            .then(function (farms) {
                allFarms = farms;
                renderAll();
            })
            .catch(function (err) {
                console.error('Failed to load farms:', err);
            });
    }

    function renderAll() {
        renderMarkers();
        renderSidebar();
    }

    // ─── Filter helpers ───────────────────────────────────────────────────────
    function getFilteredFarms() {
        return allFarms.filter(function (f) {
            var catMatch = activeCategory === 'All' || f.category === activeCategory;
            var q = searchQuery.toLowerCase();
            var searchMatch = !q || f.name.toLowerCase().includes(q) || f.category.toLowerCase().includes(q);
            return catMatch && searchMatch;
        });
    }

    // ─── Markers ──────────────────────────────────────────────────────────────
    function renderMarkers() {
        // Remove existing markers
        Object.values(markerMap).forEach(function (m) { map.removeLayer(m.marker); });
        markerMap = {};

        var filtered = getFilteredFarms();

        filtered.forEach(function (farm, index) {
            var icon = createFarmIcon(farm);
            var marker = L.marker([farm.latitude, farm.longitude], {
                icon: icon,
                riseOnHover: true
            });

            // Staggered drop-in animation via CSS delay on the icon element
            marker.on('add', function () {
                var el = marker.getElement();
                if (el) {
                    el.style.opacity = '0';
                    el.style.transform += ' translateY(-12px)';
                    el.style.transition = 'opacity 0.35s ease ' + (index * 60) + 'ms, transform 0.35s ease ' + (index * 60) + 'ms';
                    requestAnimationFrame(function () {
                        requestAnimationFrame(function () {
                            el.style.opacity = '1';
                            el.style.transform = el.style.transform.replace(' translateY(-12px)', '');
                        });
                    });
                }
            });

            marker.bindPopup(buildPopupHtml(farm), {
                maxWidth: 280,
                minWidth: 240,
                className: 'hg-popup'
            });

            marker.on('mouseover', function () {
                highlightSidebarCard(farm.id, true);
                var el = marker.getElement();
                if (el) {
                    var inner = el.querySelector('.hg-marker-inner');
                    if (inner) inner.style.transform = 'scale(1.18)';
                }
            });

            marker.on('mouseout', function () {
                highlightSidebarCard(farm.id, false);
                var el = marker.getElement();
                if (el) {
                    var inner = el.querySelector('.hg-marker-inner');
                    if (inner) inner.style.transform = '';
                }
            });

            marker.addTo(map);
            markerMap[farm.id] = { marker: marker, farm: farm };
        });

        // Update count
        var countEl = document.getElementById('sidebarCount');
        if (countEl) countEl.textContent = filtered.length;
    }

    function createFarmIcon(farm) {
        var emoji = CATEGORY_EMOJI[farm.category] || '🌱';
        var bgColor = farm.isOpen ? '#2D5016' : '#888888';

        return L.divIcon({
            className: '',
            html: [
                '<div class="hg-marker">',
                '  <div class="hg-marker-inner" style="background:' + bgColor + '">',
                '    <span class="hg-marker-emoji">' + emoji + '</span>',
                '  </div>',
                '  <div class="hg-marker-caret" style="border-top-color:' + bgColor + '"></div>',
                '</div>'
            ].join(''),
            iconSize: [40, 50],
            iconAnchor: [20, 50],
            popupAnchor: [0, -52]
        });
    }

    function buildPopupHtml(farm) {
        var emoji = CATEGORY_EMOJI[farm.category] || '🌱';
        var catColor = CATEGORY_COLOR[farm.category] || '#2D5016';
        var statusClass = farm.isOpen ? 'popup-status--open' : 'popup-status--closed';
        var statusText = farm.isOpen ? 'Open' : 'Closed';
        var stars = '';
        var full = Math.floor(farm.rating);
        var half = farm.rating - full >= 0.5;
        for (var i = 0; i < 5; i++) {
            if (i < full) stars += '<span class="popup-star filled">★</span>';
            else if (i === full && half) stars += '<span class="popup-star half">★</span>';
            else stars += '<span class="popup-star empty">★</span>';
        }

        return [
            '<div class="hg-popup-inner">',
            '  <div class="hg-popup-img-wrap">',
            '    <img src="' + farm.imageUrl + '" alt="' + escHtml(farm.name) + '" class="hg-popup-img" />',
            '    <span class="hg-popup-status ' + statusClass + '">' + statusText + '</span>',
            '  </div>',
            '  <div class="hg-popup-body">',
            '    <div class="hg-popup-cat-row">',
            '      <span class="hg-popup-cat" style="background:' + catColor + '20;color:' + catColor + '">' + emoji + ' ' + escHtml(farm.category) + '</span>',
            '      <span class="hg-popup-dist">' + farm.distanceMi + ' mi</span>',
            '    </div>',
            '    <h6 class="hg-popup-name">' + escHtml(farm.name) + '</h6>',
            '    <div class="hg-popup-rating">' + stars + ' <span class="hg-popup-rating-num">' + farm.rating.toFixed(1) + '</span></div>',
            '    <p class="hg-popup-desc">' + escHtml(farm.description.substring(0, 100)) + (farm.description.length > 100 ? '…' : '') + '</p>',
            '    <p class="hg-popup-products"><i class="bi bi-box-seam me-1"></i>' + farm.productCount + ' products available</p>',
            '    <a href="/Farms/' + farm.id + '" class="hg-popup-link">View Farm <i class="bi bi-arrow-right ms-1"></i></a>',
            '  </div>',
            '</div>'
        ].join('');
    }

    // ─── Sidebar ──────────────────────────────────────────────────────────────
    function renderSidebar() {
        var list = document.getElementById('sidebarFarmList');
        if (!list) return;

        var filtered = getFilteredFarms();
        list.innerHTML = '';

        if (filtered.length === 0) {
            list.innerHTML = '<div class="sidebar-empty"><i class="bi bi-search mb-2 d-block" style="font-size:2rem;color:#ccc"></i><p>No farms match your filters.</p></div>';
            return;
        }

        filtered.forEach(function (farm) {
            var emoji = CATEGORY_EMOJI[farm.category] || '🌱';
            var catColor = CATEGORY_COLOR[farm.category] || '#2D5016';
            var card = document.createElement('div');
            card.className = 'sidebar-farm-card';
            card.dataset.farmId = farm.id;
            card.innerHTML = [
                '<img src="' + farm.imageUrl + '" alt="' + escHtml(farm.name) + '" class="sidebar-card-img" />',
                '<div class="sidebar-card-body">',
                '  <div class="sidebar-card-top">',
                '    <span class="sidebar-card-cat" style="color:' + catColor + '">' + emoji + ' ' + escHtml(farm.category) + '</span>',
                '    <span class="sidebar-card-status ' + (farm.isOpen ? 'status-open' : 'status-closed') + '">' + (farm.isOpen ? 'Open' : 'Closed') + '</span>',
                '  </div>',
                '  <h6 class="sidebar-card-name">' + escHtml(farm.name) + '</h6>',
                '  <div class="sidebar-card-meta">',
                '    <span class="sidebar-card-rating">★ ' + farm.rating.toFixed(1) + '</span>',
                '    <span class="sidebar-card-dist">' + farm.distanceMi + ' mi away</span>',
                '  </div>',
                '  <p class="sidebar-card-products">' + farm.productCount + ' products</p>',
                '</div>'
            ].join('');

            card.addEventListener('click', function () {
                panToFarm(farm);
            });

            card.addEventListener('mouseenter', function () {
                highlightMarker(farm.id, true);
            });

            card.addEventListener('mouseleave', function () {
                highlightMarker(farm.id, false);
            });

            list.appendChild(card);
        });
    }

    function panToFarm(farm) {
        map.flyTo([farm.latitude, farm.longitude], 15, {
            animate: true,
            duration: 1.0
        });
        setTimeout(function () {
            var entry = markerMap[farm.id];
            if (entry) entry.marker.openPopup();
        }, 1100);
    }

    function highlightMarker(farmId, on) {
        var entry = markerMap[farmId];
        if (!entry) return;
        var el = entry.marker.getElement();
        if (!el) return;
        var inner = el.querySelector('.hg-marker-inner');
        if (inner) {
            inner.style.transform = on ? 'scale(1.18)' : '';
            inner.style.background = on ? '#C4622D' : (entry.farm.isOpen ? '#2D5016' : '#888888');
        }
    }

    function highlightSidebarCard(farmId, on) {
        var card = document.querySelector('.sidebar-farm-card[data-farm-id="' + farmId + '"]');
        if (!card) return;
        if (on) {
            card.classList.add('sidebar-farm-card--active');
            card.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        } else {
            card.classList.remove('sidebar-farm-card--active');
        }
    }

    // ─── Filter chips ─────────────────────────────────────────────────────────
    function bindFilterChips() {
        var chips = document.getElementById('categoryChips');
        if (!chips) return;
        chips.addEventListener('click', function (e) {
            var btn = e.target.closest('.map-chip');
            if (!btn) return;
            document.querySelectorAll('.map-chip').forEach(function (c) { c.classList.remove('active'); });
            btn.classList.add('active');
            activeCategory = btn.dataset.category;
            renderAll();
        });
    }

    // ─── Search ───────────────────────────────────────────────────────────────
    function bindSearch() {
        var input = document.getElementById('sidebarSearch');
        if (!input) return;
        input.addEventListener('input', function () {
            searchQuery = this.value.trim();
            renderAll();
        });
    }

    // ─── Recenter ─────────────────────────────────────────────────────────────
    function bindRecenter() {
        var btn = document.getElementById('recenterBtn');
        if (!btn) return;
        btn.addEventListener('click', function () {
            map.flyTo(DEFAULT_CENTER, DEFAULT_ZOOM, { animate: true, duration: 1.2 });
        });
    }

    // ─── Mobile view toggle ───────────────────────────────────────────────────
    window.showMapView = function () {
        var canvas = document.getElementById('mapCanvasWrap');
        var sidebar = document.getElementById('mapSidebar');
        if (canvas) canvas.style.display = '';
        if (sidebar) sidebar.classList.remove('sidebar-list-active');
        document.getElementById('btnMapView').classList.add('active');
        document.getElementById('btnListView').classList.remove('active');
        setTimeout(function () { map.invalidateSize(); }, 50);
    };

    window.showListView = function () {
        var canvas = document.getElementById('mapCanvasWrap');
        var sidebar = document.getElementById('mapSidebar');
        if (canvas) canvas.style.display = 'none';
        if (sidebar) sidebar.classList.add('sidebar-list-active');
        document.getElementById('btnMapView').classList.remove('active');
        document.getElementById('btnListView').classList.add('active');
    };

    // ─── Utility ──────────────────────────────────────────────────────────────
    function escHtml(str) {
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

})();
