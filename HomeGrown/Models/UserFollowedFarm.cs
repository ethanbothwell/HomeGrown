namespace HomeGrown.Models;

public class UserFollowedFarm
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FarmId { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
