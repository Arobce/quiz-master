using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
       return _userManager.FindByEmailAsync(email);
    }

    public Task AddRoleToUserAsync(ApplicationUser user, string roleName)
    {
        return _userManager.AddToRoleAsync(user, roleName);
    }
    
}