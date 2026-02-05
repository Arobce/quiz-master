using Microsoft.AspNetCore.Mvc;
using QuizMaster.Data;
using QuizMaster.Models.ViewModel.Account;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService, ApplicationDbContext dbContext)
    {
        this._authService = authService;
    }

    public IActionResult Login() => View();
    public IActionResult Register() => View();


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.RegisterAsync(model);

        if (!result.IsAuthenticated)
        {
            ModelState.AddModelError("", result.Error);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (!await _authService.LoginAsync(model))
        {
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}
