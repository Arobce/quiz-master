using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Question;
using QuizMaster.Repositories.Interfaces;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repository;
    
    public QuestionService(IQuestionRepository repository)
    {
        _repository = repository;
    }
    
    public Task AddQuestionAsync(CreateQuestionViewModel model)
    {
        var question = new Question()
        {
            QuizId = model.QuizId,
            Text = model.Text,
            Type = model.Type,
            Points = model.Points
        };

        if (model.Type == "MCQ")
        {
            question.AnswerOptions = model.Options!
                .Select((text, index) => new MCQOption
                {
                    Text = text,
                    IsCorrect = index == model.CorrectOptionIndex
                }).ToList();
        }
        
        return _repository.AddAsync(question);
    }
}