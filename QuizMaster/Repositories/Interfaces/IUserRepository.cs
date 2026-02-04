using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;

namespace QuizMaster.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task AddRoleToUserAsync(ApplicationUser user, string roleName);
}