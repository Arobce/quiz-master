namespace QuizMaster.Models.ViewModel.Quiz.Student;

public class StudentQuestionViewModel
{
    public int QuestionId { get; set; }
    
    public string Text { get; set; }
    public string Type { get; set; }
    public int Points { get; set; }
    
    // For Multiple Choice
    public List<StudentOptionViewModel>? AnswerOptions { get; set; }
}