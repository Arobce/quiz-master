using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models.ViewModel.Question;
using QuizMaster.Services.Interfaces;

namespace QuizMaster.Controllers;

[Authorize(Roles = "Teacher")]
public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;
    
    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    
    // GET: /Question/Create?quizId=5
    public IActionResult Create(int quizId)
    {
        return View(new CreateQuestionViewModel
        {
            QuizId = quizId
        });
    }

    // POST: /Question/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateQuestionViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _questionService.AddQuestionAsync(model);

        return RedirectToAction(
            "Details",
            "Quiz",
            new { id = model.QuizId }
        );
    }
    
    
}