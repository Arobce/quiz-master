namespace QuizMaster.Models.ViewModel.Quiz;

public class QuizListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ExpiresAt { get; set; }
}