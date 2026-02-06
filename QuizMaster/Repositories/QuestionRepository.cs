using QuizMaster.Data;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;
    
    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Models.Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }
}