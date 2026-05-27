using HomeGrown.Models;

namespace HomeGrown.Services;

public static class SonomaFarmsService
{
    public static readonly List<FarmMarker> Farms = new()
    {
        new FarmMarker
        {
            Id = 1,
            Name = "Sunridge Farm",
            Description = "Heritage-breed chickens and heirloom vegetables grown on the golden hills above Sonoma Valley. Known for their extraordinary deep-orange egg yolks.",
            Latitude = 38.2919,
            Longitude = -122.4580,
            Category = "Dairy & Eggs",
            ImageUrl = "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=600",
            Rating = 4.9,
            ProductCount = 8,
            IsOpen = true,
            DistanceMi = "1.2"
        },
        new FarmMarker
        {
            Id = 2,
            Name = "Petaluma Creamery",
            Description = "Award-winning artisan cheeses and cultured dairy made from the milk of grass-fed Jersey cows on the rolling hills south of Petaluma.",
            Latitude = 38.2324,
            Longitude = -122.6367,
            Category = "Dairy & Eggs",
            ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=600",
            Rating = 4.8,
            ProductCount = 12,
            IsOpen = true,
            DistanceMi = "8.4"
        },
        new FarmMarker
        {
            Id = 3,
            Name = "Sebastopol Apple Works",
            Description = "Four generations of apple farming in the heart of the Gravenstein apple country. Fresh-pressed cider, dried fruit, and heritage-variety apple boxes available weekly.",
            Latitude = 38.4025,
            Longitude = -122.8236,
            Category = "Produce",
            ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600",
            Rating = 4.7,
            ProductCount = 9,
            IsOpen = true,
            DistanceMi = "11.3"
        },
        new FarmMarker
        {
            Id = 5,
            Name = "Glen Ellen Honey Co.",
            Description = "Small-batch wildflower and manzanita honey harvested from 80 hives set among the oaks and madrones of Sonoma Mountain. Raw, unfiltered, and deeply aromatic.",
            Latitude = 38.3686,
            Longitude = -122.5219,
            Category = "Honey & Preserves",
            ImageUrl = "https://images.unsplash.com/photo-1587049352846-4a222e784d38?w=600",
            Rating = 4.9,
            ProductCount = 6,
            IsOpen = true,
            DistanceMi = "5.8"
        },
        new FarmMarker
        {
            Id = 6,
            Name = "Kenwood Pasture Co.",
            Description = "100% grass-fed beef and pasture-raised pork from a 600-acre ranch in the Sonoma Valley. Dry-aged on-site and sold direct — no middlemen, full traceability.",
            Latitude = 38.4194,
            Longitude = -122.5506,
            Category = "Meat & Poultry",
            ImageUrl = "https://images.unsplash.com/photo-1546964124-0cce460f38ef?w=600",
            Rating = 4.8,
            ProductCount = 10,
            IsOpen = false,
            DistanceMi = "9.1"
        },
        new FarmMarker
        {
            Id = 7,
            Name = "Cotati Community Gardens",
            Description = "A cooperative urban farm producing certified organic salad greens, herbs, and edible flowers for Sonoma County restaurants and home cooks alike.",
            Latitude = 38.3271,
            Longitude = -122.7077,
            Category = "Produce",
            ImageUrl = "https://images.unsplash.com/photo-1466637574441-749b8f19452f?w=600",
            Rating = 4.6,
            ProductCount = 18,
            IsOpen = true,
            DistanceMi = "14.6"
        },
        new FarmMarker
        {
            Id = 9,
            Name = "Valley of the Moon Winery",
            Description = "Boutique estate winery nestled in Sonoma Valley producing single-vineyard Pinot Noir, Chardonnay, and Zinfandel. Wine club members get first access to each small-lot release.",
            Latitude = 38.3044,
            Longitude = -122.4786,
            Category = "Wine & Beverages",
            ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=600",
            Rating = 4.9,
            ProductCount = 7,
            IsOpen = true,
            DistanceMi = "3.4"
        },
        new FarmMarker
        {
            Id = 10,
            Name = "Freestone Poultry Farm",
            Description = "Free-range duck eggs, heritage turkey, and pasture-raised whole chickens from a small family operation near the Sonoma Coast. Everything raised on non-GMO feed with no antibiotics.",
            Latitude = 38.4102,
            Longitude = -122.9403,
            Category = "Meat & Poultry",
            ImageUrl = "https://images.unsplash.com/photo-1548550023-2bdb3c5beed7?w=600",
            Rating = 4.6,
            ProductCount = 4,
            IsOpen = true,
            DistanceMi = "18.2"
        },
        new FarmMarker
        {
            Id = 11,
            Name = "Windsor Grain & Mill",
            Description = "Stone-milled heritage flour, polenta, and cornmeal from organically grown grains farmed on the Sonoma County plains. Popular with home bakers and professional chefs.",
            Latitude = 38.5477,
            Longitude = -122.8153,
            Category = "Bread & Baked Goods",
            ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600",
            Rating = 4.8,
            ProductCount = 8,
            IsOpen = false,
            DistanceMi = "19.5"
        },
        new FarmMarker
        {
            Id = 12,
            Name = "Sonoma Sprouts & Microgreens",
            Description = "Year-round microgreens, sprouts, and living herbs grown hydroponically in a zero-waste urban facility. Harvested to order and delivered within 24 hours of cutting.",
            Latitude = 38.2946,
            Longitude = -122.4608,
            Category = "Produce",
            ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600",
            Rating = 4.7,
            ProductCount = 11,
            IsOpen = true,
            DistanceMi = "0.6"
        }
    };
}
