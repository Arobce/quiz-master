using System.Security.Claims;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz;

namespace QuizMaster.Repositories.Interfaces;

public interface IQuizRepository
{
    Task AddAsync(Quiz quiz);
    Task<IEnumerable<Quiz>> GetByTeacherIdAsync(string teacherId);
    Task<Quiz?> GetByIdAsync(int quizId);
}