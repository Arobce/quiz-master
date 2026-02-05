using System.Security.Claims;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz;

namespace QuizMaster.Repositories.Interfaces;

public interface IQuizRepository
{
    Task CreateQuizAsync(CreateQuizViewModel model, ClaimsPrincipal user);
    Task<IEnumerable<QuizListItemViewModel>> GetTeacherQuizzesAsync(ClaimsPrincipal user);
    Task<QuizDetailsViewModel> GetQuizDetailsAsync(int quizId);
}