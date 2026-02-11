using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models.ViewModel.Quiz.Student;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Controllers;

public class StudentQuizController : Controller
{
    private readonly IStudentQuizService _studentQuizService;
    
    public StudentQuizController(IStudentQuizService studentQuizService)
    {
        _studentQuizService = studentQuizService;
    }
    
    // List available quizes
    public async Task<IActionResult> Index()
    {
        var quizzes = await _studentQuizService.GetAvailableQuizzesAsync();
        return View(quizzes);
    }
    
    // Start a quiz
    public async Task<IActionResult> Start(int quizId)
    {
        var attemptId = await _studentQuizService.StartQuizAsync(quizId, User);
        return RedirectToAction("Take", new { attemptId });
    }
    
    // Take a quiz
    public async Task<IActionResult> Take(int attemptId)
    {
        var quiz = await _studentQuizService.GetQuizForAttemptAsync(attemptId);
        return View(quiz);
    }
    
    // Submit a quiz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(SubmitQuizViewModel model)
    {
        await _studentQuizService.SubmitQuizAsync(model, User);
        return RedirectToAction(nameof(Result), new { attemptId = model.AttemptId });
    }
    
    // View quiz result
    public async Task<IActionResult> Result(int attemptId)
    {
        var result = await _studentQuizService.GetResultAsync(attemptId, User);
        return View(result);
    }
    
    
}