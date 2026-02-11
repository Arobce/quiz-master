namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class StudentQuizResultViewModel
{
    public int AttemptId { get; set; }

    public string QuizTitle { get; set; }

    public int TotalScore { get; set; }
    public int MaxScore { get; set; }

    public List<StudentQuestionResultViewModel> Questions { get; set; }
        = new();
}