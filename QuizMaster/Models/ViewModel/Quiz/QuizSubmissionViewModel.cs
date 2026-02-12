namespace QuizMaster.Models.ViewModel.Quiz;

public class QuizSubmissionViewModel
{
    public int AttemptId { get; set; }
    public string StudentName { get; set; }
    public int TotalScore { get; set; }
    public int MaxScore { get; set; }
    public DateTime SubmittedAt { get; set; }
}
