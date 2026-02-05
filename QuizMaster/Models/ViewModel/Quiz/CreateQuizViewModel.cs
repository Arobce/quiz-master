using System.ComponentModel.DataAnnotations;

namespace QuizMaster.Models.ViewModel.Quiz;

public class CreateQuizViewModel
{
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
}