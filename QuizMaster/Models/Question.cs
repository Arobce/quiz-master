namespace QuizMaster.Models;

public class Question
{
    public int Id { get; set; }
    
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    
    public string Text { get; set; }
    
    public string Type { get; set; } // e.g., "MultipleChoice", "Text", etc
    
    public int Points { get; set; }
    
    // For Multiple Choice Questions
    public ICollection<MCQOption>? AnswerOptions { get; set; }
    
    // For Text Questions
    public string? SampleAnswer { get; set; }
}