using QuizMaster.Models.ViewModel.Quiz.Student;

namespace QuizMaster.Models.ViewModel.Quiz;

public class QuizSubmissionDetailViewModel
{
    public int AttemptId { get; set; }
    public int QuizId { get; set; }
    public string QuizTitle { get; set; }
    public string StudentName { get; set; }
    public int TotalScore { get; set; }
    public int MaxScore { get; set; }
    public DateTime SubmittedAt { get; set; }

    public List<StudentQuestionResultViewModel> Questions { get; set; }
        = new();
}
