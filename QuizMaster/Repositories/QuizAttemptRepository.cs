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
            .Include(q => q.Student)
            .Include(q => q.Quiz)
                .ThenInclude(q => q.Questions)
                .ThenInclude(q => q.AnswerOptions)
            .Include(q => q.Answers)
                .ThenInclude(a => a.Question)
                .ThenInclude(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == quizAttemptId);
    }

    public Task<List<QuizAttempt>> GetByQuizIdAsync(int quizId)
    {
        return _dbContext.QuizAttempts
            .Include(a => a.Student)
            .Include(a => a.Answers)
                .ThenInclude(sa => sa.Question)
            .Where(a => a.QuizId == quizId)
            .ToListAsync();
    }

    public Task<List<QuizAttempt>> GetByStudentIdAsync(string studentId)
    {
        return _dbContext.QuizAttempts
            .Include(a => a.Quiz)
            .Include(a => a.Answers)
                .ThenInclude(sa => sa.Question)
            .Where(a => a.StudentId == studentId && a.SubmittedAt != default)
            .OrderByDescending(a => a.SubmittedAt)
            .ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}