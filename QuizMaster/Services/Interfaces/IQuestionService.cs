using QuizMaster.Models.ViewModel.Question;

namespace QuizMaster.Services.Interfaces;

public interface IQuestionService
{
    Task AddQuestionAsync(CreateQuestionViewModel question);
}