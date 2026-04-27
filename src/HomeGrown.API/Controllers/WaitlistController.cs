using HomeGrown.Core.Domain.Enums;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

public record CommunityStatsDto(
    string Name,
    int FarmerCount,
    int BuyerCount,
    int FarmerTarget,
    int BuyerTarget,
    int TotalCount,
    int TotalTarget,
    bool Unlocked
);

public record WaitlistStatsDto(
    int FarmerCount,
    int BuyerCount,
    int FarmerTarget,
    int BuyerTarget,
    int TotalCount,
    int TotalTarget,
    bool Unlocked,
    IEnumerable<CommunityStatsDto> Communities
);

[ApiController]
[Route("api/waitlist")]
public class WaitlistController(IUnitOfWork uow, IConfiguration config) : ControllerBase
{
    private int FarmerTarget => config.GetValue<int>("Waitlist:FarmerTarget", 10);
    private int BuyerTarget  => config.GetValue<int>("Waitlist:BuyerTarget",  100);

    /// <summary>GET /api/waitlist/stats — public, no auth required</summary>
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var farmerCount = await uow.Users.CountByRoleAsync(UserRole.Farmer);
        var buyerCount  = await uow.Users.CountByRoleAsync(UserRole.Buyer);

        var communityRows = await uow.Users.GetCommunitySummariesAsync();
        var communities = communityRows
            .OrderByDescending(c => c.FarmerCount + c.BuyerCount)
            .Select(c => new CommunityStatsDto(
                Name:         c.Community,
                FarmerCount:  c.FarmerCount,
                BuyerCount:   c.BuyerCount,
                FarmerTarget: FarmerTarget,
                BuyerTarget:  BuyerTarget,
                TotalCount:   c.FarmerCount + c.BuyerCount,
                TotalTarget:  FarmerTarget + BuyerTarget,
                Unlocked:     c.FarmerCount >= FarmerTarget && c.BuyerCount >= BuyerTarget
            ));

        var stats = new WaitlistStatsDto(
            FarmerCount:  farmerCount,
            BuyerCount:   buyerCount,
            FarmerTarget: FarmerTarget,
            BuyerTarget:  BuyerTarget,
            TotalCount:   farmerCount + buyerCount,
            TotalTarget:  FarmerTarget + BuyerTarget,
            Unlocked:     farmerCount >= FarmerTarget && buyerCount >= BuyerTarget,
            Communities:  communities
        );

        return Ok(stats);
    }

    /// <summary>GET /api/waitlist/stats/{community} — stats for a specific community</summary>
    [HttpGet("stats/{community}")]
    public async Task<IActionResult> GetCommunityStats(string community)
    {
        var farmerCount = await uow.Users.CountByRoleAsync(UserRole.Farmer, community);
        var buyerCount  = await uow.Users.CountByRoleAsync(UserRole.Buyer,  community);

        var stats = new CommunityStatsDto(
            Name:         community,
            FarmerCount:  farmerCount,
            BuyerCount:   buyerCount,
            FarmerTarget: FarmerTarget,
            BuyerTarget:  BuyerTarget,
            TotalCount:   farmerCount + buyerCount,
            TotalTarget:  FarmerTarget + BuyerTarget,
            Unlocked:     farmerCount >= FarmerTarget && buyerCount >= BuyerTarget
        );

        return Ok(stats);
    }
}
