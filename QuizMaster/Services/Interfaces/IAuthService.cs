using QuizMaster.Models.ViewModel.Account;

namespace QuizMaster.Services.Interfaces;

public interface IAuthService
{
    Task<(bool IsAuthenticated, string Error)> RegisterAsync(RegisterViewModel model);
    Task<bool> LoginAsync(LoginViewModel model);
    Task LogoutAsync();
}