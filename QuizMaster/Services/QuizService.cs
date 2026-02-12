using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Question;
using QuizMaster.Models.ViewModel.Quiz;
using QuizMaster.Models.ViewModel.Quiz.Student;
using QuizMaster.Repositories.Interfaces;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizAttemptRepository _quizAttemptRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public QuizService(
        IQuizRepository quizRepository,
        IQuizAttemptRepository quizAttemptRepository,
        UserManager<ApplicationUser> userManager)
    {
        _quizRepository = quizRepository;
        _quizAttemptRepository = quizAttemptRepository;
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

    public async Task<QuizDetailsViewModel> GetQuizDetailsAsync(int quizId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null)
        {
            throw new KeyNotFoundException($"Quiz {quizId} not found");
        }

        var attempts = await _quizAttemptRepository.GetByQuizIdAsync(quizId);

        return new QuizDetailsViewModel
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Questions = quiz.Questions?
                .Select(q => new QuestionListItemViewModel
                {
                    Text = q.Text,
                    Type = q.Type,
                    Points = q.Points
                })
                .ToList() ?? new List<QuestionListItemViewModel>(),
            Submissions = attempts.Select(a => new QuizSubmissionViewModel
            {
                AttemptId = a.Id,
                StudentName = a.Student.FullName,
                TotalScore = a.Answers.Sum(ans => ans.Score ?? 0),
                MaxScore = a.Answers.Sum(ans => ans.Question.Points),
                SubmittedAt = a.SubmittedAt
            }).ToList()
        };
    }

    public async Task<QuizSubmissionDetailViewModel> GetSubmissionDetailAsync(int attemptId)
    {
        var attempt = await _quizAttemptRepository.GetByIdAsync(attemptId);
        if (attempt == null)
        {
            throw new KeyNotFoundException($"Attempt {attemptId} not found");
        }

        return new QuizSubmissionDetailViewModel
        {
            AttemptId = attempt.Id,
            QuizId = attempt.QuizId,
            QuizTitle = attempt.Quiz.Title,
            StudentName = attempt.Student?.FullName ?? "Unknown",
            TotalScore = attempt.Answers.Sum(a => a.Score ?? 0),
            MaxScore = attempt.Quiz.Questions.Sum(q => q.Points),
            SubmittedAt = attempt.SubmittedAt,
            Questions = attempt.Answers.Select(a => new StudentQuestionResultViewModel
            {
                QuestionText = a.Question.Text,
                StudentAnswer = a.Question.Type == "MCQ"
                    ? a.Question.AnswerOptions?.FirstOrDefault(o => o.Id == a.SelectedOptionId)?.Text
                    : a.AnswerText,
                CorrectAnswer = a.Question.Type == "MCQ"
                    ? a.Question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect)?.Text
                    : a.Question.SampleAnswer,
                Points = a.Question.Points,
                Score = a.Score,
                IsAiGraded = a.IsAiGraded
            }).ToList()
        };
    }
    
    private string GetUserId(ClaimsPrincipal user)
    {
        return _userManager.GetUserId(user) ?? throw new Exception("User not found");
    }
}
