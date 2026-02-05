using QuizMaster.Models.ViewModel.Question;

namespace QuizMaster.Models.ViewModel.Quiz;

public class QuizDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }

    public List<QuestionListItemViewModel> Questions { get; set; }
        = new();
}
