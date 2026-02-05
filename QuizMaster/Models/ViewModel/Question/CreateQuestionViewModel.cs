using System.ComponentModel.DataAnnotations;

namespace QuizMaster.Models.ViewModel.Question;

public class CreateQuestionViewModel
{
    [Required]
    public int QuizId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Text { get; set; }

    [Required]
    public string Type { get; set; } // "MCQ" | "Text"

    [Range(1, 100)]
    public int Points { get; set; }

    // MCQ only
    public List<string>? Options { get; set; }

    public int? CorrectOptionIndex { get; set; }
}