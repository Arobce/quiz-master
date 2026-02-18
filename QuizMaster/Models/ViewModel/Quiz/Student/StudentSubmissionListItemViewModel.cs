namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class StudentSubmissionListItemViewModel
{
    public int AttemptId { get; set; }
    public string QuizTitle { get; set; }
    public int TotalScore { get; set; }
    public int MaxScore { get; set; }
    public DateTime SubmittedAt { get; set; }
}
