using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models.ViewModel.Quiz;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Controllers;

[Authorize(Roles = "Teacher")]
public class QuizController : Controller
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    public async Task<IActionResult> Index()
    {
        var quizzes = await _quizService.GetAllTeacherQuizzesAsync(User);

        return View(quizzes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateQuizViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _quizService.CreateQuizAsync(model, User);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var quiz = await _quizService.GetQuizDetailsAsync(id);
            return View(quiz);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}
