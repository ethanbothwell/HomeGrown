// HomeGrown — Site JavaScript

document.addEventListener('DOMContentLoaded', function () {

    // ===== INTERSECTION OBSERVER: fade-in-up =====
    var fadeElements = document.querySelectorAll('.fade-in-up');
    if (fadeElements.length > 0) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                    observer.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.12,
            rootMargin: '0px 0px -40px 0px'
        });
        fadeElements.forEach(function (el) {
            observer.observe(el);
        });
    }

    // ===== STICKY NAVBAR SHADOW ON SCROLL =====
    var nav = document.getElementById('mainNav');
    if (nav) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 20) {
                nav.classList.add('scrolled');
            } else {
                nav.classList.remove('scrolled');
            }
        }, { passive: true });
    }

    // ===== QUANTITY STEPPER (product detail page) =====
    var qtyInputs = document.querySelectorAll('.qty-input');
    qtyInputs.forEach(function (input) {
        var wrapper = input.closest('.qty-stepper');
        if (!wrapper) return;
        var minus = wrapper.querySelector('.qty-minus');
        var plus = wrapper.querySelector('.qty-plus');
        if (minus) {
            minus.addEventListener('click', function () {
                var val = parseInt(input.value) || 1;
                if (val > 1) input.value = val - 1;
            });
        }
        if (plus) {
            plus.addEventListener('click', function () {
                var val = parseInt(input.value) || 1;
                var max = parseInt(input.max) || 99;
                if (val < max) input.value = val + 1;
            });
        }
        input.addEventListener('change', function () {
            var val = parseInt(this.value) || 1;
            var min = parseInt(this.min) || 1;
            var max = parseInt(this.max) || 99;
            if (val < min) this.value = min;
            if (val > max) this.value = max;
        });
    });

    // ===== CART QUANTITY STEPPERS (cart page — button type submit) =====
    // The cart uses form submit buttons, so no extra JS needed.
    // Just highlight row on hover.

    // ===== AUTO-DISMISS ALERTS =====
    var alerts = document.querySelectorAll('.alert-auto-dismiss');
    alerts.forEach(function (alert) {
        setTimeout(function () {
            alert.style.transition = 'opacity 0.5s ease';
            alert.style.opacity = '0';
            setTimeout(function () { alert.remove(); }, 500);
        }, 4000);
    });

    // ===== CARD NUMBER FORMATTING (checkout) =====
    var cardInput = document.querySelector('input[name="CardNumber"]');
    if (cardInput) {
        cardInput.addEventListener('input', function () {
            var val = this.value.replace(/\D/g, '').substring(0, 16);
            this.value = val.replace(/(.{4})/g, '$1 ').trim();
        });
    }

    var expiryInput = document.querySelector('input[name="CardExpiry"]');
    if (expiryInput) {
        expiryInput.addEventListener('input', function () {
            var val = this.value.replace(/\D/g, '').substring(0, 4);
            if (val.length >= 3) {
                this.value = val.substring(0, 2) + ' / ' + val.substring(2);
            } else {
                this.value = val;
            }
        });
    }

    // ===== WISHLIST BUTTON TOGGLE =====
    document.querySelectorAll('.wishlist-btn').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var icon = this.querySelector('i');
            if (icon) {
                if (icon.classList.contains('bi-heart')) {
                    icon.classList.replace('bi-heart', 'bi-heart-fill');
                    icon.style.color = '#e74c3c';
                } else {
                    icon.classList.replace('bi-heart-fill', 'bi-heart');
                    icon.style.color = '';
                }
            }
        });
    });

    // ===== NEWSLETTER FORM =====
    document.querySelectorAll('.newsletter-form').forEach(function (form) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            var input = form.querySelector('input[type="email"]');
            var btn = form.querySelector('button[type="submit"]');
            var email = input ? input.value.trim() : '';

            // Client-side email validation
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            var errorEl = form.querySelector('.newsletter-error');
            if (errorEl) errorEl.remove();

            if (!email || !emailPattern.test(email)) {
                var err = document.createElement('p');
                err.className = 'newsletter-error';
                err.textContent = 'Please enter a valid email address.';
                err.style.cssText = 'color:#C4622D;font-size:0.82rem;margin-top:8px;margin-bottom:0;';
                form.appendChild(err);
                return;
            }

            // Loading state
            var originalText = btn ? btn.textContent : 'Subscribe';
            if (btn) { btn.textContent = 'Subscribing...'; btn.disabled = true; }
            if (input) input.disabled = true;

            fetch('/api/subscribe', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email: email })
            })
            .then(function (res) { return res.json(); })
            .then(function (data) {
                var wrapper = form.closest('.newsletter-form-wrapper') || form.parentElement;
                var msg = document.createElement('div');
                msg.className = 'newsletter-success-msg';

                if (data.duplicate) {
                    msg.textContent = "You're already part of the family! 🌱";
                } else if (data.success) {
                    msg.textContent = "Welcome to the HomeGrown community — we'll be in touch soon.";
                } else {
                    // Show inline error, don't replace form
                    if (btn) { btn.textContent = originalText; btn.disabled = false; }
                    if (input) { input.disabled = false; }
                    var err2 = document.createElement('p');
                    err2.className = 'newsletter-error';
                    err2.textContent = data.message || 'Something went wrong. Please try again.';
                    err2.style.cssText = 'color:#C4622D;font-size:0.82rem;margin-top:8px;margin-bottom:0;';
                    form.appendChild(err2);
                    return;
                }

                // Replace form with confirmation message
                form.style.transition = 'opacity 0.3s ease';
                form.style.opacity = '0';
                setTimeout(function () {
                    form.replaceWith(msg);
                    // Trigger fade-in
                    requestAnimationFrame(function () {
                        msg.style.opacity = '0';
                        msg.style.transform = 'translateY(8px)';
                        msg.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
                        requestAnimationFrame(function () {
                            msg.style.opacity = '1';
                            msg.style.transform = 'translateY(0)';
                        });
                    });
                }, 300);
            })
            .catch(function () {
                if (btn) { btn.textContent = originalText; btn.disabled = false; }
                if (input) { input.disabled = false; }
                var err3 = document.createElement('p');
                err3.className = 'newsletter-error';
                err3.textContent = 'Something went wrong. Please try again.';
                err3.style.cssText = 'color:#C4622D;font-size:0.82rem;margin-top:8px;margin-bottom:0;';
                form.appendChild(err3);
            });
        });
    });

    // ===== SMOOTH SCROLL for anchor links =====
    document.querySelectorAll('a[href^="#"]').forEach(function (link) {
        link.addEventListener('click', function (e) {
            var target = document.querySelector(this.getAttribute('href'));
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        });
    });

    // ===== ACCOUNT TYPE CARD STYLING =====
    document.querySelectorAll('.account-type-radio').forEach(function (radio) {
        radio.addEventListener('change', function () {
            document.querySelectorAll('.account-type-card').forEach(function (card) {
                card.style.borderColor = '';
                card.style.background = '';
            });
            if (this.checked) {
                var card = this.nextElementSibling;
                if (card) {
                    card.style.borderColor = '#2D5016';
                    card.style.background = 'rgba(45,80,22,0.06)';
                }
            }
        });
        // Initialize checked state
        if (radio.checked) {
            var card = radio.nextElementSibling;
            if (card) {
                card.style.borderColor = '#2D5016';
                card.style.background = 'rgba(45,80,22,0.06)';
            }
        }
    });

});
