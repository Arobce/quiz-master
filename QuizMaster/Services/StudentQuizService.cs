using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Quiz.Student;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Services;

public class StudentQuizService : IStudentQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizAttemptRepository _quizAttemptRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public StudentQuizService(
        IQuizRepository quizRepo,
        IQuestionRepository questionRepo,
        IQuizAttemptRepository quizAttemptRepo,
        UserManager<ApplicationUser> userManager)
    {
        _quizRepository = quizRepo;
        _questionRepository = questionRepo;
        _userManager = userManager;
        _quizAttemptRepository = quizAttemptRepo;
    }
    
    // List available quizzes for students
    public async Task<IEnumerable<StudentQuizListItemViewModel>> GetAvailableQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetALlAsync();
        var quizList = quizzes.Select(q => new StudentQuizListItemViewModel
        {
            QuizId = q.Id,
            Title = q.Title,
            TotalPoints = q.Questions.Sum(x => x.Points),
        });
        
        return quizList;
    }
    
    
    public async Task<int> StartQuizAsync(int quizId, ClaimsPrincipal student)
    {
        var user = await _userManager.GetUserAsync(student);

        var attempt = new QuizAttempt
        {
            QuizId = quizId,
            StudentId = user.Id,
            StartedAt = DateTime.UtcNow,
        };
        
        await _quizAttemptRepository.AddAsync(attempt);
        return attempt.Id;

    }

    public async Task<StudentTakeQuizViewModel> GetQuizForAttemptAsync(int attemptId)
    {
        var attempt = await _quizAttemptRepository.GetByIdAsync(attemptId);

        return new StudentTakeQuizViewModel
        {
            AttemptId = attempt.Id,
            QuizId = attempt.QuizId,
            Title = attempt.Quiz.Title,

            Questions = attempt.Quiz.Questions
                .Select(q => new StudentQuestionViewModel
                {
                    QuestionId = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Points = q.Points,

                    AnswerOptions = q.Type == "MCQ"
                        ? (q.AnswerOptions ?? new List<MCQOption>())
                        .Select(a => new StudentOptionViewModel
                        {
                            OptionId = a.Id,
                            Text = a.Text
                        })
                        .ToList()
                        : new List<StudentOptionViewModel>()
                })
                .ToList()
        };
    }
    
    // Submit and Grade
    public async Task SubmitQuizAsync(SubmitQuizViewModel model, ClaimsPrincipal student)
    {
        var user = await _userManager.GetUserAsync(student);
        var attempt = await _quizAttemptRepository.GetByIdAsync(model.AttemptId);

        if (attempt == null)
        {
            throw new InvalidOperationException("Quiz attempt not found.");
        }
        
        // Ownership check
        if(attempt.StudentId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only submit your own quiz attempts.");
        }

        foreach (var answerVm in model.Answers)
        {
            var question = await _questionRepository.GetByIdAsync(answerVm.QuestionId);

            var answer = new StudentAnswer
            {
                QuizAttemptId = attempt.Id,
                QuestionId = answerVm.QuestionId,
            };
            
            if (question.Type == "MCQ")
            {
                answer.SelectedOptionId = answerVm.SelectedOptionId;
                
                // Get correct option
                var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);
                var selectedOption = question.AnswerOptions?.FirstOrDefault(o => o.Id == answerVm.SelectedOptionId);

                if (correctOption == null || selectedOption == null)
                {
                    answer.Score = 0;
                }
                else
                {
                    answer.Score = correctOption.Id == selectedOption.Id ? question.Points : 0;
                }
                answer.IsAiGraded = false;
            }
            else
            {
                answer.AnswerText = answerVm.AnswerText;
                
                // Ai grading
                answer.IsAiGraded = true;
                answer.Score = question.Points;
            }
            
            attempt.Answers.Add(answer);
        }

        attempt.SubmittedAt = DateTime.UtcNow;
        await _quizAttemptRepository.SaveAsync();
    }
    
    // View result
    public async Task<StudentQuizResultViewModel> GetResultAsync(int attemptId, ClaimsPrincipal student)
    {
        var user = await _userManager.GetUserAsync(student);
        var attempt = await _quizAttemptRepository.GetByIdAsync(attemptId);
        
        if(attempt.StudentId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only view your own quiz results.");
        }

        var questions = attempt.Answers.Select(a => new StudentQuestionResultViewModel
        {
            QuestionText = a.Question.Text,
            StudentAnswer = a.Question.Type == "MCQ"
                ? (a.Question.AnswerOptions.FirstOrDefault(o => o.Id == a.SelectedOptionId)?.Text ?? "No answer")
                : a.AnswerText,
            CorrectAnswer = a.Question.Type == "MCQ"
                ? (a.Question.AnswerOptions.FirstOrDefault(o => o.IsCorrect)?.Text ?? "N/A")
                : "N/A",
            Points = a.Question.Points,
            Score = a.Score,
            IsAiGraded = a.IsAiGraded
        }).ToList();
        
        return new StudentQuizResultViewModel
        {
            AttemptId = attempt.Id,
            QuizTitle = attempt.Quiz.Title,
            TotalScore = attempt.Answers.Sum(a => a.Score ?? 0),
            Questions = questions,
            MaxScore = questions.Sum(a => a.Points),
        };
    }
}
