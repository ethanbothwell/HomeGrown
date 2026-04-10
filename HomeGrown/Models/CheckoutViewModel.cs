namespace HomeGrown.Models;

public class CheckoutViewModel
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Zip { get; set; } = "";
    public string CardNumber { get; set; } = "";
    public string CardExpiry { get; set; } = "";
    public string CardCvv { get; set; } = "";
    public List<CartItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}
