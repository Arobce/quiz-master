namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class SubmitQuizViewModel
{
    public int AttemptId { get; set; }

    public List<SubmitAnswerViewModel> Answers { get; set; }
        = new();
}