using QuizMaster.Models;

namespace QuizMaster.Tests.Models;

public class QuestionTests
{
    [Fact]
    public void Question_McqType_HasAnswerOptions()
    {
        var question = new Question
        {
            Id = 1,
            QuizId = 1,
            Text = "What is 2 + 2?",
            Type = "MCQ",
            Points = 5,
            AnswerOptions = new List<MCQOption>
            {
                new() { Id = 1, Text = "3", IsCorrect = false },
                new() { Id = 2, Text = "4", IsCorrect = true },
            }
        };

        Assert.Equal(2, question.AnswerOptions.Count);
        Assert.Single(question.AnswerOptions, o => o.IsCorrect);
    }

    [Fact]
    public void Question_TextType_HasSampleAnswer()
    {
        var question = new Question
        {
            Id = 2,
            QuizId = 1,
            Text = "Explain polymorphism",
            Type = "Text",
            Points = 10,
            SampleAnswer = "Polymorphism allows objects to take many forms"
        };

        Assert.Equal("Text", question.Type);
        Assert.NotNull(question.SampleAnswer);
        Assert.Null(question.AnswerOptions);
    }
}
