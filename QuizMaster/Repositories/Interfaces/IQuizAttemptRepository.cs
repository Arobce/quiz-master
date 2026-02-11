using QuizMaster.Models;

namespace QuizMaster.Repositories.Interfaces;

public interface IQuizAttemptRepository
{
    Task AddAsync(QuizAttempt quizAttempt);
    Task<QuizAttempt?> GetByIdAsync(int quizAttemptId);
    Task SaveAsync();
}