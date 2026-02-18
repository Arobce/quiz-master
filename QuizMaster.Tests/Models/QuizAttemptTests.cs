using QuizMaster.Models;

namespace QuizMaster.Tests.Models;

public class QuizAttemptTests
{
    [Fact]
    public void QuizAttempt_Answers_DefaultsToEmptyList()
    {
        var attempt = new QuizAttempt();

        Assert.NotNull(attempt.Answers);
        Assert.Empty(attempt.Answers);
    }

    [Fact]
    public void QuizAttempt_StartedAt_DefaultsToUtcNow()
    {
        var before = DateTime.UtcNow;
        var attempt = new QuizAttempt();
        var after = DateTime.UtcNow;

        Assert.InRange(attempt.StartedAt, before, after);
    }
}
