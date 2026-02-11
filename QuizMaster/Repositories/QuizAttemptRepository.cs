using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Repositories;

public class QuizAttemptRepository : IQuizAttemptRepository
{
    private readonly ApplicationDbContext _dbContext;

    public QuizAttemptRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(QuizAttempt quizAttempt)
    {
        _dbContext.QuizAttempts.Add(quizAttempt);
        await _dbContext.SaveChangesAsync();
    }

    public Task<QuizAttempt?> GetByIdAsync(int quizAttemptId)
    {
        return _dbContext.QuizAttempts
            .Include(q=> q.Quiz)
            .ThenInclude(q => q.Questions)
            .ThenInclude(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == quizAttemptId);
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}