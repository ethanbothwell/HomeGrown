using HomeGrown.Models;

namespace HomeGrown.Services;

public class MockDataService
{
    public static readonly List<Farm> Farms = new()
    {
        new Farm
        {
            Id = 1, Name = "Sunridge Farm", OwnerName = "Maria Thornton",
            Location = "Sonoma County, CA", City = "Sonoma", State = "CA",
            Bio = "Nestled in the golden hills of Sonoma County, Sunridge Farm has been a family operation for three generations. We raise heritage chickens and grow heirloom vegetables using only the most sustainable methods.",
            Philosophy = "\"We believe the land is a living thing. Treat it with care and it will provide abundantly for generations to come.\"",
            ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=1200",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=49",
            Latitude = 38.2919, Longitude = -122.4580, DistanceMiles = "12",
            Practices = new List<string> { "Pasture-Raised", "No Pesticides", "Heirloom Varieties", "Cover Cropping" },
            Rating = 4.9, ReviewCount = 87
        },
        new Farm
        {
            Id = 2, Name = "Blue Hen Hollow", OwnerName = "James Whitfield",
            Location = "Asheville, NC", City = "Asheville", State = "NC",
            Bio = "Tucked into the Blue Ridge foothills, Blue Hen Hollow is a small but passionate operation focused on humane poultry practices and farm-fresh eggs. James started the farm after leaving a tech career to reconnect with the land.",
            Philosophy = "\"A happy hen lays the best egg. We let our birds roam free, eat well, and live naturally.\"",
            ImageUrl = "https://images.unsplash.com/photo-1464226184884-fa280b87c399?w=1200",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=51",
            Latitude = 35.5951, Longitude = -82.5515, DistanceMiles = "8",
            Practices = new List<string> { "Free-Range", "No Antibiotics", "Non-GMO Feed", "Humane Certified" },
            Rating = 4.8, ReviewCount = 63
        },
        new Farm
        {
            Id = 3, Name = "The Kneaded Loaf", OwnerName = "Sofia Reyes",
            Location = "Portland, OR", City = "Portland", State = "OR",
            Bio = "The Kneaded Loaf is Portland's beloved artisan bakery sourcing local grains and milling their own flour on-site. Sofia trained in France and brought those traditions back to the Pacific Northwest.",
            Philosophy = "\"Good bread takes time. We let our sourdoughs ferment for 48 hours because flavor and nutrition are worth waiting for.\"",
            ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=53",
            Latitude = 45.5051, Longitude = -122.6750, DistanceMiles = "3",
            Practices = new List<string> { "Stone-Milled Flour", "Long Fermentation", "Local Grains", "No Preservatives" },
            Rating = 5.0, ReviewCount = 142
        },
        new Farm
        {
            Id = 4, Name = "Wild Creek Honey", OwnerName = "Earl Mason",
            Location = "Blue Ridge, GA", City = "Blue Ridge", State = "GA",
            Bio = "Wild Creek Honey keeps over 200 hives in the pristine Blue Ridge Mountains. Earl is a third-generation beekeeper who also crafts small-batch fruit preserves using only honey as a sweetener.",
            Philosophy = "\"Every jar of honey tells the story of the wildflowers our bees visited. It's nature in a bottle.\"",
            ImageUrl = "https://images.unsplash.com/photo-1587049352846-4a222e784d38?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=55",
            Latitude = 34.8651, Longitude = -84.3244, DistanceMiles = "22",
            Practices = new List<string> { "Treatment-Free", "Wildflower Forage", "No Additives", "Small-Batch" },
            Rating = 4.7, ReviewCount = 56
        },
        new Farm
        {
            Id = 5, Name = "Mossy Oak Pastures", OwnerName = "Greg Halverson",
            Location = "Bozeman, MT", City = "Bozeman", State = "MT",
            Bio = "Spanning 800 acres of Montana grassland, Mossy Oak Pastures raises 100% grass-fed beef and heritage pork. The cattle rotate pastures daily in a regenerative system that sequesters carbon and builds soil health.",
            Philosophy = "\"Grass-fed, grass-finished, and raised with respect. Our animals live full lives on open pasture before they become your dinner.\"",
            ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=1200",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=57",
            Latitude = 45.6770, Longitude = -111.0429, DistanceMiles = "34",
            Practices = new List<string> { "100% Grass-Fed", "Regenerative Grazing", "No Hormones", "Certified Humane" },
            Rating = 4.9, ReviewCount = 74
        },
        new Farm
        {
            Id = 6, Name = "Valley Goat Creamery", OwnerName = "Claire Dubois",
            Location = "Napa Valley, CA", City = "Napa", State = "CA",
            Bio = "Claire fell in love with goat cheese during a year spent in Provence and returned home to build her own small herd in the Napa Valley. The creamery produces award-winning aged and fresh chèvre.",
            Philosophy = "\"Goat cheese is an art form. Our animals graze on native grasses and herbs, and that flavor shows in every bite.\"",
            ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=59",
            Latitude = 38.2975, Longitude = -122.2869, DistanceMiles = "18",
            Practices = new List<string> { "Artisan Crafted", "Pasture-Grazed", "No rBGH", "Small Herd" },
            Rating = 4.8, ReviewCount = 91
        },
        new Farm
        {
            Id = 7, Name = "Morning Dew Gardens", OwnerName = "Priya Nair",
            Location = "Burlington, VT", City = "Burlington", State = "VT",
            Bio = "Morning Dew Gardens grows over 60 varieties of herbs and specialty produce in Vermont's Lake Champlain valley. Priya combines traditional Indian growing practices with Vermont's rich agricultural heritage.",
            Philosophy = "\"Every herb has a story and a purpose. We grow them with intention and harvest them at their peak so you get all the flavor and medicine nature intended.\"",
            ImageUrl = "https://images.unsplash.com/photo-1466637574441-749b8f19452f?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=61",
            Latitude = 44.4759, Longitude = -73.2121, DistanceMiles = "7",
            Practices = new List<string> { "Certified Organic", "Biodynamic", "Heirloom Seeds", "Hand-Harvested" },
            Rating = 4.6, ReviewCount = 48
        },
        new Farm
        {
            Id = 8, Name = "Copper Kettle Farms", OwnerName = "Ruth Ann Briggs",
            Location = "Lexington, KY", City = "Lexington", State = "KY",
            Bio = "Three generations of the Briggs family have been making preserves the old-fashioned way — small batches, copper kettles, and recipes passed down since 1946. Ruth Ann expanded the operation to include local honey and seasonal gift boxes.",
            Philosophy = "\"A jar of our jam is a jar of summer. We put the whole season in there — the sunshine, the rain, the love.\"",
            ImageUrl = "https://images.unsplash.com/photo-1584483766114-2cea6facdf57?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=63",
            Latitude = 38.0406, Longitude = -84.5037, DistanceMiles = "15",
            Practices = new List<string> { "Small-Batch", "Traditional Recipes", "No Artificial Pectin", "Local Fruit" },
            Rating = 4.7, ReviewCount = 103
        },
        new Farm
        {
            Id = 9, Name = "Riverbend Organics", OwnerName = "Tom Keller",
            Location = "Hudson Valley, NY", City = "Kingston", State = "NY",
            Bio = "Set along the banks of the Hudson River, Riverbend Organics has been certified organic for over 20 years. Tom grows an incredible diversity of vegetables for farmers markets and a 200-member CSA.",
            Philosophy = "\"Organic isn't just a label for us — it's a commitment to our soil, our community, and the future. If the soil is healthy, everything else follows.\"",
            ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=65",
            Latitude = 41.7004, Longitude = -73.9209, DistanceMiles = "28",
            Practices = new List<string> { "Certified Organic", "No-Till", "Cover Crops", "Composting" },
            Rating = 4.8, ReviewCount = 117
        },
        new Farm
        {
            Id = 10, Name = "Prairie Wind Ranch", OwnerName = "Cole Dawson",
            Location = "Wichita, KS", City = "Wichita", State = "KS",
            Bio = "Prairie Wind Ranch covers the open plains of south-central Kansas with thousands of acres of native grassland supporting grass-fed beef cattle and pasture-raised lamb. Cole's family has farmed the same land for four generations.",
            Philosophy = "\"The prairie made us. We're just stewards of what was already here. If we manage it well, it'll outlast all of us.\"",
            ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=1200",
            OwnerImageUrl = "https://i.pravatar.cc/150?img=67",
            Latitude = 37.6872, Longitude = -97.3301, DistanceMiles = "42",
            Practices = new List<string> { "Native Grassland", "Grass-Finished", "No Feedlot", "Hormone-Free" },
            Rating = 4.6, ReviewCount = 59
        }
    };

    public static readonly List<Product> Products = new()
    {
        new Product
        {
            Id = 1, Name = "Free-Range Brown Eggs (Dozen)", FarmId = 1, FarmName = "Sunridge Farm",
            FarmLocation = "Sonoma County, CA", Category = "Produce",
            Price = 7.50m, Unit = "dozen",
            Description = "Our hens roam the hills of Sonoma freely, pecking at bugs, grasses, and seeds all day long. The result? Eggs with deep golden yolks and exceptional flavor. Collected fresh each morning.",
            ImageUrl = "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=600",
            InStock = true, Tags = "eggs,free-range,pasture-raised"
        },
        new Product
        {
            Id = 2, Name = "Heirloom Tomato Medley (2 lbs)", FarmId = 1, FarmName = "Sunridge Farm",
            FarmLocation = "Sonoma County, CA", Category = "Produce",
            Price = 9.00m, Unit = "2 lbs",
            Description = "A colorful mix of Cherokee Purple, Green Zebra, and Sun Gold tomatoes grown from heirloom seed. Bursting with flavor you simply cannot find in a grocery store.",
            ImageUrl = "https://images.unsplash.com/photo-1592841200221-a6898f307baa?w=600&h=400&fit=crop",
            InStock = true, Tags = "tomatoes,heirloom,organic,vegetables"
        },
        new Product
        {
            Id = 3, Name = "Heritage Breed Dozen Eggs", FarmId = 2, FarmName = "Blue Hen Hollow",
            FarmLocation = "Asheville, NC", Category = "Produce",
            Price = 8.00m, Unit = "dozen",
            Description = "Eggs from our Barred Plymouth Rock and Rhode Island Red hens. These heritage breeds produce eggs with thick shells, rich flavor, and vibrant orange yolks that chefs adore.",
            ImageUrl = "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=600",
            InStock = true, Tags = "eggs,heritage,free-range"
        },
        new Product
        {
            Id = 4, Name = "Whole Pasture-Raised Chicken (4-5 lbs)", FarmId = 2, FarmName = "Blue Hen Hollow",
            FarmLocation = "Asheville, NC", Category = "Meat",
            Price = 22.00m, Unit = "each",
            Description = "Our chickens spend their entire lives on pasture in the Blue Ridge hills. Slow-raised for flavor and texture, these birds will change the way you think about chicken.",
            ImageUrl = "https://images.unsplash.com/photo-1548550023-2bdb3c5beed7?w=600",
            InStock = true, Tags = "chicken,pasture-raised,whole-bird"
        },
        new Product
        {
            Id = 5, Name = "Country Sourdough Loaf", FarmId = 3, FarmName = "The Kneaded Loaf",
            FarmLocation = "Portland, OR", Category = "Bread & Baked",
            Price = 12.00m, Unit = "loaf",
            Description = "Our flagship sourdough. Stone-milled local wheat, 48-hour cold fermentation, and baked in a wood-fired deck oven. Open crumb, crackly crust, and complex tangy flavor.",
            ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600&h=400&fit=crop",
            InStock = true, Tags = "sourdough,bread,artisan,organic"
        },
        new Product
        {
            Id = 6, Name = "Seeded Rye Boule", FarmId = 3, FarmName = "The Kneaded Loaf",
            FarmLocation = "Portland, OR", Category = "Bread & Baked",
            Price = 13.00m, Unit = "loaf",
            Description = "Dark, dense, and loaded with caraway, sunflower, and poppy seeds. Made with 40% whole rye flour for a deeply nutty flavor. Perfect with smoked fish or aged cheese.",
            ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=600",
            InStock = true, Tags = "rye,bread,seeded,artisan"
        },
        new Product
        {
            Id = 7, Name = "Wildflower Honey (16 oz)", FarmId = 4, FarmName = "Wild Creek Honey",
            FarmLocation = "Blue Ridge, GA", Category = "Honey & Preserves",
            Price = 16.00m, Unit = "16 oz jar",
            Description = "Raw, unfiltered wildflower honey from our Blue Ridge Mountain hives. Never heated, never blended. Each batch varies with the seasons — spring brings lighter floral notes, summer deeper and bolder.",
            ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=600&h=400&fit=crop",
            InStock = true, Tags = "honey,raw,wildflower,unfiltered"
        },
        new Product
        {
            Id = 8, Name = "Sourwood Honey (12 oz)", FarmId = 4, FarmName = "Wild Creek Honey",
            FarmLocation = "Blue Ridge, GA", Category = "Honey & Preserves",
            Price = 19.00m, Unit = "12 oz jar",
            Description = "Rare single-origin sourwood honey harvested in midsummer when the sourwood trees bloom in the Appalachian hollows. Buttery, spicy, and considered the finest honey in North America.",
            ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=600&h=400&fit=crop",
            InStock = true, Tags = "honey,sourwood,single-origin,rare"
        },
        new Product
        {
            Id = 9, Name = "Grass-Fed Ground Beef (1 lb)", FarmId = 5, FarmName = "Mossy Oak Pastures",
            FarmLocation = "Bozeman, MT", Category = "Meat",
            Price = 14.00m, Unit = "1 lb",
            Description = "80/20 blend from 100% grass-fed and grass-finished Montana cattle. Rich, beefy flavor with a healthy fat profile. No antibiotics, no hormones, no feedlot — ever.",
            ImageUrl = "https://images.unsplash.com/photo-1546964124-0cce460f38ef?w=600",
            InStock = true, Tags = "beef,grass-fed,ground,Montana"
        },
        new Product
        {
            Id = 10, Name = "Heritage Pork Chops (2-pack)", FarmId = 5, FarmName = "Mossy Oak Pastures",
            FarmLocation = "Bozeman, MT", Category = "Meat",
            Price = 18.00m, Unit = "2 pack",
            Description = "Thick-cut bone-in chops from our heritage Berkshire and Duroc pigs. Raised on open pasture with room to root and roam. Intensely marbled and flavorful — nothing like commodity pork.",
            ImageUrl = "https://images.unsplash.com/photo-1529692236671-f1f6cf9683ba?w=600",
            InStock = false, Tags = "pork,heritage,pasture-raised,chops"
        },
        new Product
        {
            Id = 11, Name = "Fresh Chèvre Log (6 oz)", FarmId = 6, FarmName = "Valley Goat Creamery",
            FarmLocation = "Napa Valley, CA", Category = "Dairy",
            Price = 11.00m, Unit = "6 oz",
            Description = "Creamy, bright, and tangy fresh goat cheese made from the milk of our Nubian and LaMancha goats. Rolled in fresh herbs from our garden. Pairs beautifully with honey or baguette.",
            ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=600",
            InStock = true, Tags = "cheese,goat,fresh,chevre,dairy"
        },
        new Product
        {
            Id = 12, Name = "Aged Ash-Rolled Goat Cheese (4 oz)", FarmId = 6, FarmName = "Valley Goat Creamery",
            FarmLocation = "Napa Valley, CA", Category = "Dairy",
            Price = 15.00m, Unit = "4 oz",
            Description = "Our showpiece aged cheese — coated in activated charcoal ash and aged 3 weeks. Firm yet creamy, earthy and complex. A wine country staple.",
            ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=600",
            InStock = true, Tags = "cheese,goat,aged,ash,dairy"
        },
        new Product
        {
            Id = 13, Name = "Fresh Herb Bundle (Basil, Thyme, Rosemary)", FarmId = 7, FarmName = "Morning Dew Gardens",
            FarmLocation = "Burlington, VT", Category = "Produce",
            Price = 6.00m, Unit = "bundle",
            Description = "A generous bundle of our most-loved cooking herbs, harvested at sunrise when the oils are at their most fragrant. Certified organic, hand-tied, and delivered same-day.",
            ImageUrl = "https://images.unsplash.com/photo-1466637574441-749b8f19452f?w=600",
            InStock = true, Tags = "herbs,basil,thyme,rosemary,organic"
        },
        new Product
        {
            Id = 14, Name = "Mixed Salad Greens (5 oz)", FarmId = 7, FarmName = "Morning Dew Gardens",
            FarmLocation = "Burlington, VT", Category = "Produce",
            Price = 7.00m, Unit = "5 oz bag",
            Description = "A lively mix of 15 varieties — arugula, mizuna, tatsoi, frisée, and more. Harvested with scissors at baby-leaf stage for maximum tenderness. Washed and ready to eat.",
            ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600",
            InStock = true, Tags = "salad,greens,organic,vegetables"
        },
        new Product
        {
            Id = 17, Name = "Rainbow Chard Bunch", FarmId = 9, FarmName = "Riverbend Organics",
            FarmLocation = "Hudson Valley, NY", Category = "Produce",
            Price = 5.50m, Unit = "bunch",
            Description = "Brilliant stems of red, yellow, orange, and white chard harvested from our organic river-bottom beds. Mild and sweet, excellent sautéed with garlic or added raw to salads.",
            ImageUrl = "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=600",
            InStock = true, Tags = "chard,organic,vegetables,greens"
        },
        new Product
        {
            Id = 18, Name = "CSA Vegetable Box (Small)", FarmId = 9, FarmName = "Riverbend Organics",
            FarmLocation = "Hudson Valley, NY", Category = "Produce",
            Price = 28.00m, Unit = "box",
            Description = "A curated selection of 6-8 seasonal vegetables from our certified organic farm. This week's box includes zucchini, beets, kale, cucumber, tomatoes, and sweet corn. Contents vary weekly.",
            ImageUrl = "https://images.unsplash.com/photo-1488459716781-31db52582fe9?w=1200",
            InStock = true, Tags = "csa,vegetables,organic,seasonal,box"
        },
        new Product
        {
            Id = 19, Name = "Grass-Fed Ribeye Steak (12 oz)", FarmId = 10, FarmName = "Prairie Wind Ranch",
            FarmLocation = "Wichita, KS", Category = "Meat",
            Price = 26.00m, Unit = "12 oz steak",
            Description = "A thick-cut ribeye from our native-grassland cattle. Grass-finished means more omega-3s, more CLA, and a uniquely beefy, mineral-rich flavor that grain-fed beef simply cannot match.",
            ImageUrl = "https://images.unsplash.com/photo-1546964124-0cce460f38ef?w=600",
            InStock = true, Tags = "beef,ribeye,grass-fed,steak"
        },
        new Product
        {
            Id = 20, Name = "Pasture-Raised Lamb Rack (1 lb)", FarmId = 10, FarmName = "Prairie Wind Ranch",
            FarmLocation = "Wichita, KS", Category = "Meat",
            Price = 24.00m, Unit = "1 lb",
            Description = "Spring lamb raised on native prairie grasses and finished on alfalfa. Mild, tender, and subtly sweet — a world apart from imported lamb. Perfect for a special occasion roast.",
            ImageUrl = "https://images.unsplash.com/photo-1514516345957-556ca7d90a29?w=600&h=400&fit=crop",
            InStock = true, Tags = "lamb,pasture-raised,rack,grass-fed"
        },
        new Product
        {
            Id = 21, Name = "Croissant 4-Pack", FarmId = 3, FarmName = "The Kneaded Loaf",
            FarmLocation = "Portland, OR", Category = "Bread & Baked",
            Price = 14.00m, Unit = "4 pack",
            Description = "Classic butter croissants made with local organic butter and stone-milled flour. 36 hours of lamination produces those impossibly flaky, honeycomb layers. Baked fresh each morning.",
            ImageUrl = "https://images.unsplash.com/photo-1530610476181-d83430b64dcd?w=600&h=400&fit=crop",
            InStock = true, Tags = "croissant,pastry,butter,baked"
        },
        new Product
        {
            Id = 22, Name = "Raw Clover Honey (8 oz)", FarmId = 4, FarmName = "Wild Creek Honey",
            FarmLocation = "Blue Ridge, GA", Category = "Honey & Preserves",
            Price = 11.00m, Unit = "8 oz jar",
            Description = "Classic raw clover honey with a mild, clean sweetness. Perfect as an everyday honey for tea, baking, or spreading on toast. Unheated to preserve natural enzymes and antioxidants.",
            ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=600&h=400&fit=crop",
            InStock = true, Tags = "honey,clover,raw,unfiltered"
        }
    };

    public static readonly List<AppUser> Users = new()
    {
        new AppUser { Id = 1, Name = "Alex Johnson", Email = "alex@example.com", PasswordHash = "password123", IsFarmer = false },
        new AppUser { Id = 2, Name = "Sarah Chen", Email = "sarah@example.com", PasswordHash = "password123", IsFarmer = false },
        new AppUser { Id = 3, Name = "Mike Davis", Email = "mike@example.com", PasswordHash = "password123", IsFarmer = false },
        new AppUser { Id = 4, Name = "Maria Thornton", Email = "maria@sunridge.com", PasswordHash = "farmpass1", IsFarmer = true, FarmId = 1 },
        new AppUser { Id = 5, Name = "James Whitfield", Email = "james@bluehen.com", PasswordHash = "farmpass2", IsFarmer = true, FarmId = 2 },
        new AppUser { Id = 6, Name = "Sofia Reyes", Email = "sofia@kneadedloaf.com", PasswordHash = "farmpass3", IsFarmer = true, FarmId = 3 },
        new AppUser { Id = 7, Name = "Earl Mason", Email = "earl@wildcreek.com", PasswordHash = "farmpass4", IsFarmer = true, FarmId = 4 },
        new AppUser { Id = 8, Name = "Greg Halverson", Email = "greg@mossyoak.com", PasswordHash = "farmpass5", IsFarmer = true, FarmId = 5 }
    };

    public static readonly List<Review> Reviews = new()
    {
        // Sunridge Farm
        new Review { Id = 1, FarmId = 1, ReviewerName = "Amanda K.", AvatarUrl = "https://i.pravatar.cc/150?img=1", Rating = 5, Text = "Maria's eggs are absolutely life-changing. The yolks are so dark orange and rich — my kids refuse to eat store eggs now. Worth every penny.", Date = new DateTime(2026, 2, 14) },
        new Review { Id = 2, FarmId = 1, ReviewerName = "Derek T.", AvatarUrl = "https://i.pravatar.cc/150?img=3", Rating = 5, Text = "The heirloom tomatoes are stunning. I made a caprese salad and my guests couldn't believe it. Sunridge is my go-to every summer.", Date = new DateTime(2026, 1, 20) },
        new Review { Id = 3, FarmId = 1, ReviewerName = "Jen M.", AvatarUrl = "https://i.pravatar.cc/150?img=5", Rating = 4, Text = "Consistently great quality. Maria is always responsive and packs everything beautifully. A true treasure in Sonoma County.", Date = new DateTime(2025, 12, 5) },
        // Blue Hen Hollow
        new Review { Id = 4, FarmId = 2, ReviewerName = "Chris R.", AvatarUrl = "https://i.pravatar.cc/150?img=7", Rating = 5, Text = "The whole chicken was incredible. Roasted it simply with herbs and the flavor was miles beyond anything from the supermarket. James is doing it right.", Date = new DateTime(2026, 2, 28) },
        new Review { Id = 5, FarmId = 2, ReviewerName = "Laura B.", AvatarUrl = "https://i.pravatar.cc/150?img=9", Rating = 5, Text = "Best eggs in Western North Carolina, hands down. The heritage breeds make such a difference. We've been ordering monthly for a year.", Date = new DateTime(2026, 1, 15) },
        new Review { Id = 6, FarmId = 2, ReviewerName = "Paul N.", AvatarUrl = "https://i.pravatar.cc/150?img=11", Rating = 4, Text = "Quality is superb and James is a genuine, passionate farmer. Delivery was a little slow once but he made it right immediately.", Date = new DateTime(2025, 11, 22) },
        // The Kneaded Loaf
        new Review { Id = 7, FarmId = 3, ReviewerName = "Nina W.", AvatarUrl = "https://i.pravatar.cc/150?img=13", Rating = 5, Text = "I've eaten bread in Paris. Sofia's sourdough is on that level. The crust, the crumb, the tang — perfection. I order two loaves every week.", Date = new DateTime(2026, 3, 1) },
        new Review { Id = 8, FarmId = 3, ReviewerName = "Tom H.", AvatarUrl = "https://i.pravatar.cc/150?img=15", Rating = 5, Text = "The seeded rye is extraordinary. Dense, complex, nutty. A revelation. The croissants are flawless. This bakery is Portland's best-kept secret... or it was.", Date = new DateTime(2026, 2, 10) },
        new Review { Id = 9, FarmId = 3, ReviewerName = "Beth O.", AvatarUrl = "https://i.pravatar.cc/150?img=17", Rating = 5, Text = "Changed my whole relationship with bread. I now bake sourdough myself because of Sofia's inspiration. Still buy hers because mine will never be as good.", Date = new DateTime(2026, 1, 5) },
        // Wild Creek Honey
        new Review { Id = 10, FarmId = 4, ReviewerName = "Rachel S.", AvatarUrl = "https://i.pravatar.cc/150?img=21", Rating = 5, Text = "The sourwood honey is liquid gold. I've given jars as gifts and everyone demands to know where I got it. Remarkable product.", Date = new DateTime(2026, 2, 20) },
        new Review { Id = 11, FarmId = 4, ReviewerName = "Gary P.", AvatarUrl = "https://i.pravatar.cc/150?img=23", Rating = 4, Text = "Earl's wildflower honey has a depth of flavor I've never found in commercial honey. The seasonal variation makes each jar a new experience.", Date = new DateTime(2026, 1, 10) },
        // Mossy Oak Pastures
        new Review { Id = 12, FarmId = 5, ReviewerName = "Susan M.", AvatarUrl = "https://i.pravatar.cc/150?img=25", Rating = 5, Text = "I was a lifelong grain-fed beef person until I tried Mossy Oak's grass-finished ribeye. The mineral flavor, the color, the texture — just different. Converting my whole family.", Date = new DateTime(2026, 3, 2) },
        new Review { Id = 13, FarmId = 5, ReviewerName = "Dan C.", AvatarUrl = "https://i.pravatar.cc/150?img=27", Rating = 5, Text = "Greg ships the meat in perfect condition — frozen solid, thoughtfully packed. The quality is exceptional and knowing how the animals are raised matters to our family.", Date = new DateTime(2026, 2, 5) },
        // Valley Goat Creamery
        new Review { Id = 14, FarmId = 6, ReviewerName = "Isabel F.", AvatarUrl = "https://i.pravatar.cc/150?img=29", Rating = 5, Text = "Claire's ash-rolled goat cheese stopped conversation at my dinner party. Everyone wanted to know where it came from. Already reordered.", Date = new DateTime(2026, 2, 25) },
        new Review { Id = 15, FarmId = 6, ReviewerName = "Mark D.", AvatarUrl = "https://i.pravatar.cc/150?img=31", Rating = 4, Text = "The fresh chèvre is creamy and bright — perfect on crusty bread with a drizzle of honey. A weekly staple in our house now.", Date = new DateTime(2026, 1, 18) },
        // Morning Dew Gardens
        new Review { Id = 16, FarmId = 7, ReviewerName = "Emily K.", AvatarUrl = "https://i.pravatar.cc/150?img=33", Rating = 5, Text = "Priya's herb bundles smell like a garden in the best possible way. So fresh you can taste the difference in every dish. The salad mix is also outstanding.", Date = new DateTime(2026, 2, 14) },
        new Review { Id = 17, FarmId = 7, ReviewerName = "Rob J.", AvatarUrl = "https://i.pravatar.cc/150?img=35", Rating = 4, Text = "Really excellent variety and quality. The herbs last much longer in the fridge than anything from the grocery store. Clear dedication to craft.", Date = new DateTime(2026, 1, 22) },
        // Copper Kettle Farms
        new Review { Id = 18, FarmId = 8, ReviewerName = "Grace T.", AvatarUrl = "https://i.pravatar.cc/150?img=37", Rating = 5, Text = "Ruth Ann's preserves are the best I've ever had. I've reordered multiple times. Pure quality in every jar.", Date = new DateTime(2026, 3, 1) },
        new Review { Id = 19, FarmId = 8, ReviewerName = "Phil A.", AvatarUrl = "https://i.pravatar.cc/150?img=39", Rating = 5, Text = "There is real family heritage in these preserves. You can taste the generations of recipe refinement. Nothing artificial, nothing shortcut. Pure quality.", Date = new DateTime(2026, 2, 8) },
        // Riverbend Organics
        new Review { Id = 20, FarmId = 9, ReviewerName = "Carla N.", AvatarUrl = "https://i.pravatar.cc/150?img=41", Rating = 5, Text = "Tom's CSA box is the highlight of our week. The vegetables are so fresh and the variety is incredible. 20+ years of organic farming shows in everything he grows.", Date = new DateTime(2026, 2, 20) },
        new Review { Id = 21, FarmId = 9, ReviewerName = "Kevin B.", AvatarUrl = "https://i.pravatar.cc/150?img=43", Rating = 4, Text = "Riverbend sets the standard for organic produce in the Hudson Valley. The chard and kale are especially exceptional — tender and flavorful.", Date = new DateTime(2026, 1, 30) },
        // Prairie Wind Ranch
        new Review { Id = 22, FarmId = 10, ReviewerName = "Anna M.", AvatarUrl = "https://i.pravatar.cc/150?img=45", Rating = 5, Text = "The prairie lamb rack was the best lamb I've ever cooked at home. Incredibly clean flavor, perfectly trimmed. Cole clearly loves what he does.", Date = new DateTime(2026, 2, 15) },
        new Review { Id = 23, FarmId = 10, ReviewerName = "Dave S.", AvatarUrl = "https://i.pravatar.cc/150?img=47", Rating = 4, Text = "Great grass-fed ribeye with real depth of flavor. Leaner than grain-fed but the taste makes up for it. Will definitely keep ordering.", Date = new DateTime(2026, 1, 25) }
    };

    // Instance methods for convenience
    public List<Farm> GetFarms() => Farms;
    public List<Product> GetProducts() => Products;
    public List<Review> GetReviews() => Reviews;
    public List<AppUser> GetUsers() => Users;

    public Farm? GetFarm(int id) => Farms.FirstOrDefault(f => f.Id == id);
    public Product? GetProduct(int id) => Products.FirstOrDefault(p => p.Id == id);

    public List<Product> GetProductsByFarm(int farmId) =>
        Products.Where(p => p.FarmId == farmId).ToList();

    public List<Review> GetReviewsByFarm(int farmId) =>
        Reviews.Where(r => r.FarmId == farmId).ToList();

    public List<Product> GetFeaturedProducts(int count = 6) =>
        Products.Where(p => p.InStock).Take(count).ToList();

    public List<Product> FilterProducts(string? category, decimal? minPrice, decimal? maxPrice, bool inStockOnly, string? sort)
    {
        var query = Products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (inStockOnly)
            query = query.Where(p => p.InStock);

        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.Id)
        };

        return query.ToList();
    }

    public AppUser? AuthenticateUser(string email, string password) =>
        Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.PasswordHash == password);

    public AppUser? GetUserByEmail(string email) =>
        Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
}
