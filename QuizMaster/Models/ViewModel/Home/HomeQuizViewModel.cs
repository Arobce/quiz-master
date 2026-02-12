namespace QuizMaster.Models.ViewModel.Home;

public class HomeQuizViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TeacherName { get; set; }
    public int QuestionCount { get; set; }
    public int TotalPoints { get; set; }
    public DateTime CreatedAt { get; set; }
}
