namespace QuizMaster.Models;

public class Quiz
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string TeacherId { get; set; }
    public ApplicationUser Teacher { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    
    public ICollection<Question> Questions { get; set; }
}