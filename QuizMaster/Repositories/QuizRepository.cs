using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public QuizRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task CreateQuizAsync(CreateQuizViewModel model, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QuizListItemViewModel>> GetTeacherQuizzesAsync(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    public Task<QuizDetailsViewModel> GetQuizDetailsAsync(int quizId)
    {
        throw new NotImplementedException();
    }
}