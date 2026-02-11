namespace QuizMaster.Models.ViewModel.Quiz.Student;


public class StudentTakeQuizViewModel
{
    public int AttemptId { get; set; }

    public int QuizId { get; set; }
    public string Title { get; set; }

    public List<StudentQuestionViewModel> Questions { get; set; }
        = new();
}