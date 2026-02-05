using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Account;
using QuizMaster.Repositories.Interfaces;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        IUserRepository userRepo,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepo;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // ----------------------------
    // REGISTER
    // ----------------------------
    public async Task<(bool IsAuthenticated, string Error)> RegisterAsync(RegisterViewModel model)
    {
        var allowedRoles = new[] { "Teacher", "Student" };
        if (!allowedRoles.Contains(model.Role))
            return (false, "Invalid role");

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userRepository.CreateUserAsync(user, model.Password);
        if (!result.Succeeded)
            return (false, result.Errors.First().Description);

        await _userRepository.AddRoleToUserAsync(user, model.Role);

        // ADD CLAIM + SIGN IN
        await EnsureFullNameClaimAsync(user);
        await _signInManager.SignInAsync(user, false);

        return (true, user.Id);
    }

    // ----------------------------
    // LOGIN
    // ----------------------------
    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            false);

        if (!result.Succeeded)
            return false;

        // 👇 Fetch user + refresh claims
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            await EnsureFullNameClaimAsync(user);
        }

        return true;
    }

    // ----------------------------
    // LOGOUT
    // ----------------------------
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    // ----------------------------
    // CLAIM HANDLER
    // ----------------------------
    private async Task EnsureFullNameClaimAsync(ApplicationUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        if (!claims.Any(c => c.Type == "FullName"))
        {
            await _userManager.AddClaimAsync(
                user,
                new Claim("FullName", user.FullName)
            );
        }

        // Refresh auth cookie so claim appears in User
        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(user, isPersistent: false);
    }
}
