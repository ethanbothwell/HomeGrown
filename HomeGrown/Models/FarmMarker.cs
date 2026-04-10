namespace HomeGrown.Models;

public class FarmMarker
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Category { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public double Rating { get; set; }
    public int ProductCount { get; set; }
    public bool IsOpen { get; set; }
    public string DistanceMi { get; set; } = "";
}
