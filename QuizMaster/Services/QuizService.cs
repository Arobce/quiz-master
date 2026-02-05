using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz;
using QuizMaster.Repositories.Interfaces;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public QuizService(IQuizRepository quizRepository, UserManager<ApplicationUser> userManager)
    {
        _quizRepository = quizRepository;
        _userManager = userManager;
    }
    
    public async Task CreateQuizAsync(CreateQuizViewModel model, ClaimsPrincipal user)
    {
        var teacherId = GetUserId(user);
        
        var quiz = new Quiz
        {
            Title = model.Title,
            Description = model.Description,
            TeacherId = teacherId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = model.ExpiresAt,
        };
        
        await _quizRepository.AddAsync(quiz);
    }

    public async Task<IEnumerable<QuizListItemViewModel>> GetAllTeacherQuizzesAsync(ClaimsPrincipal user)
    {
        var teacherId = GetUserId(user);
        
        var quizzes = await _quizRepository.GetByTeacherIdAsync(teacherId);
        
        return quizzes.Select(q => new QuizListItemViewModel
        {
            Id = q.Id,
            Title = q.Title,
            CreatedAt = q.CreatedAt,
            ExpiresAt = q.ExpiresAt
        });
    }

    public Task<QuizDetailsViewModel> GetQuizDetailsAsync(int quizId)
    {
        throw new NotImplementedException();
    }
    
    private string GetUserId(ClaimsPrincipal user)
    {
        return _userManager.GetUserId(user) ?? throw new Exception("User not found");
    }
}