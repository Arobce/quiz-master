using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationDbContext _dbContext;

    public QuizRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Quiz quiz)
    {
        _dbContext.Quizzes.Add(quiz);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Quiz>> GetByTeacherIdAsync(string teacherId)
    {
        return await _dbContext.Quizzes
            .Where(q => q.TeacherId == teacherId)
            .ToListAsync();
    }

    public async Task<Quiz?> GetByIdAsync(int quizId)
    {
        return await _dbContext.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == quizId);
    }
}