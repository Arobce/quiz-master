namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class StudentQuestionResultViewModel
{
    public string QuestionText { get; set; }

    public string? StudentAnswer { get; set; }
    public string? CorrectAnswer { get; set; }

    public int Points { get; set; }
    public int? Score { get; set; }

    public bool IsAiGraded { get; set; }
}