namespace QuizMaster.Models;

public class QuizAttempt
{
    public int Id { get; set; }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public string StudentId { get; set; }
    public ApplicationUser Student { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime SubmittedAt { get; set; }

    public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();
}
