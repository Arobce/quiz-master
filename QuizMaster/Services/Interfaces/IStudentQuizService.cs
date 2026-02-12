using System.Security.Claims;
using QuizMaster.Models.ViewModel.Quiz.Student;

namespace QuizMaster.Repositories.Interfaces;

public interface IStudentQuizService
{
    Task<IEnumerable<StudentQuizListItemViewModel>> GetAvailableQuizzesAsync();
    Task<int> StartQuizAsync(int quizId, ClaimsPrincipal student);
    Task<StudentTakeQuizViewModel> GetQuizForAttemptAsync(int attemptId);
    Task SubmitQuizAsync(SubmitQuizViewModel model, ClaimsPrincipal student);
    Task<StudentQuizResultViewModel> GetResultAsync(int attemptId, ClaimsPrincipal student);
    Task<IEnumerable<StudentSubmissionListItemViewModel>> GetMySubmissionsAsync(ClaimsPrincipal student);
}
