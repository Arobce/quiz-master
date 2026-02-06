using QuizMaster.Models;

namespace QuizMaster.Repositories.Interfaces;

public interface IQuestionRepository
{
    Task AddAsync(Question question);
}