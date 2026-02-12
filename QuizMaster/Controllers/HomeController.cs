using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Models.ViewModel.Home;
using QuizMaster.Repositories.Interfaces;

namespace QuizMaster.Controllers;

public class HomeController : Controller
{
    private readonly IQuizRepository _quizRepository;

    public HomeController(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<IActionResult> Index()
    {
        var quizzes = await _quizRepository.GetALlAsync();

        var viewModel = quizzes.Select(q => new HomeQuizViewModel
        {
            Id = q.Id,
            Title = q.Title,
            Description = q.Description,
            TeacherName = q.Teacher?.FullName ?? "Unknown",
            QuestionCount = q.Questions?.Count ?? 0,
            TotalPoints = q.Questions?.Sum(question => question.Points) ?? 0,
            CreatedAt = q.CreatedAt
        });

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
