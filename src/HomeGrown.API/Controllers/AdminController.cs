using HomeGrown.Core.Application.DTOs.Auth;
using HomeGrown.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeGrown.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController(IUnitOfWork uow) : ControllerBase
{
    /// <summary>GET /api/admin/users — list all users</summary>
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await uow.Users.GetAllAsync();
        return Ok(users.Select(u => new UserDto(u.Id, u.Name, u.Email, u.Role.ToString(), u.ProfileImageUrl)));
    }

    /// <summary>DELETE /api/admin/users/{id} — remove a user</summary>
    [HttpDelete("users/{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await uow.Users.GetByIdAsync(id);
        if (user is null) return NotFound();

        uow.Users.Remove(user);
        await uow.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>GET /api/admin/farms — list all farms including inactive</summary>
    [HttpGet("farms")]
    public async Task<IActionResult> GetAllFarms()
    {
        var farms = await uow.Farms.GetAllAsync();
        return Ok(farms.Select(f => new { f.Id, f.Name, f.OwnerId, f.IsActive, f.CreatedAt }));
    }
}
