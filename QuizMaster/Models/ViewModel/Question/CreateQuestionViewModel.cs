using System.ComponentModel.DataAnnotations;

namespace QuizMaster.Models.ViewModel.Question;

public class CreateQuestionViewModel
{
    [Required]
    public int QuizId { get; set; }

    [Required(ErrorMessage = "Question text is required.")]
    [StringLength(1000, ErrorMessage = "Question text cannot exceed 1000 characters.")]
    public string Text { get; set; }

    [Required(ErrorMessage = "Please select a question type.")]
    public string Type { get; set; } // "MCQ" | "Text"

    [Required(ErrorMessage = "Points are required.")]
    [Range(1, 100, ErrorMessage = "Points must be between 1 and 100.")]
    public int? Points { get; set; }

    // MCQ only
    public List<string>? Options { get; set; }

    public int? CorrectOptionIndex { get; set; }
}