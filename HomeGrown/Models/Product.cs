namespace HomeGrown.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int FarmId { get; set; }
    public string FarmName { get; set; } = "";
    public string FarmLocation { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public string Unit { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public bool InStock { get; set; }
    public string Tags { get; set; } = "";
}
