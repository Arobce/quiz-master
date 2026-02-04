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

    public AuthService(
        IUserRepository userRepo,
        SignInManager<ApplicationUser> signInManager)
    {
        _userRepository = userRepo;
        _signInManager = signInManager;
    }

    
    public async Task<(bool IsAuthenticated, string Error)> RegisterAsync(RegisterViewModel model)
    {
        var allowedRoles = new [] {"Teacher", "Student"};
        if (!allowedRoles.Contains(model.Role))
            return (false, "Invalid role");

        
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };
        
        var result = await _userRepository.CreateUserAsync(user, model.Password);
        
        if (!result.Succeeded) return (false, result.Errors.First().Description);
        
        await _userRepository.AddRoleToUserAsync(user, model.Role);
        await _signInManager.SignInAsync(user, false);
        
        return (true, user.Id);
    }

    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            false);

        return result.Succeeded;    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}