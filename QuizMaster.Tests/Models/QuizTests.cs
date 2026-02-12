using QuizMaster.Models;

namespace QuizMaster.Tests.Models;

public class QuizTests
{
    [Fact]
    public void Quiz_CreatedAt_DefaultsToUtcNow()
    {
        var before = DateTime.UtcNow;
        var quiz = new Quiz();
        var after = DateTime.UtcNow;

        Assert.InRange(quiz.CreatedAt, before, after);
    }

    [Fact]
    public void Quiz_Properties_CanBeSetAndRead()
    {
        var quiz = new Quiz
        {
            Id = 1,
            Title = "C# Basics",
            Description = "A quiz on C# fundamentals",
            TeacherId = "teacher-123",
            ExpiresAt = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc)
        };

        Assert.Equal(1, quiz.Id);
        Assert.Equal("C# Basics", quiz.Title);
        Assert.Equal("A quiz on C# fundamentals", quiz.Description);
        Assert.Equal("teacher-123", quiz.TeacherId);
        Assert.Equal(new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), quiz.ExpiresAt);
    }
}
