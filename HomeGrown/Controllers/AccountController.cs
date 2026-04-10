using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using HomeGrown.Services;

namespace HomeGrown.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _users;

    public AccountController(IUserService users)
    {
        _users = users;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _users.AuthenticateAsync(email, password);
        if (user == null)
        {
            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        await SignInUser(user);

        return user.IsFarmer
            ? RedirectToAction("Index", "FarmerDashboard")
            : RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string name, string email, string password, string accountType)
    {
        if (await _users.GetByEmailAsync(email) != null)
        {
            ViewBag.Error = "An account with that email already exists.";
            return View();
        }

        var isFarmer = accountType == "farmer";
        var user = await _users.RegisterAsync(name, email, password, isFarmer);
        await SignInUser(user);

        return isFarmer
            ? RedirectToAction("Index", "FarmerDashboard")
            : RedirectToAction("Index", "Dashboard");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private async Task SignInUser(Models.AppUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new("IsFarmer", user.IsFarmer.ToString()),
            new("FarmId", user.FarmId?.ToString() ?? "")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = true });
    }
}
