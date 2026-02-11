namespace QuizMaster.Models;

public class StudentAnswer
{
    public int Id { get; set; }
    
    public int QuizAttemptId { get; set; }
    public QuizAttempt QuizAttempt { get; set; }
    
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    
    // MCQ
    public int? SelectedOptionId { get; set; }
    
    // Text
    public string? AnswerText { get; set; }
    
    // Grading
    public int? Score { get; set; }
    public bool IsAiGraded { get; set; }
}

