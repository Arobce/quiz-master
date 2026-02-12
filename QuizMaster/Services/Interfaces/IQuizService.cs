using System.Security.Claims;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz;

namespace QuizMaster.Services.Interfaces;

public interface IQuizService
{
    public Task CreateQuizAsync(CreateQuizViewModel model, ClaimsPrincipal user);
    public Task<IEnumerable<QuizListItemViewModel>> GetAllTeacherQuizzesAsync(ClaimsPrincipal user);
    public Task<QuizDetailsViewModel> GetQuizDetailsAsync(int quizId);
    public Task<QuizSubmissionDetailViewModel> GetSubmissionDetailAsync(int attemptId);

}