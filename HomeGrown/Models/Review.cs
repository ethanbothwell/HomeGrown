namespace HomeGrown.Models;

public class Review
{
    public int Id { get; set; }
    public int FarmId { get; set; }
    public string ReviewerName { get; set; } = "";
    public string AvatarUrl { get; set; } = "";
    public int Rating { get; set; }
    public string Text { get; set; } = "";
    public DateTime Date { get; set; }
}
