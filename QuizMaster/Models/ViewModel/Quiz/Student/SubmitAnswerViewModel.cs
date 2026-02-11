namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class SubmitAnswerViewModel
{
    public int QuestionId { get; set; }

    // MCQ
    public int? SelectedOptionId { get; set; }

    // Text
    public string? AnswerText { get; set; }
}