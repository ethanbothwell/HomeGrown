namespace HomeGrown.Models;

public class Farm
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string OwnerName { get; set; } = "";
    public string Location { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Bio { get; set; } = "";
    public string Philosophy { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string OwnerImageUrl { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string DistanceMiles { get; set; } = "";
    public List<string> Practices { get; set; } = new();
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
}
