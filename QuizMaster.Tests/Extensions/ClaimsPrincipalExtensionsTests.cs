using System.Security.Claims;
using QuizMaster.Extensions;

namespace QuizMaster.Tests.Extensions;

public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void FullName_ReturnsClaim_WhenPresent()
    {
        var claims = new[] { new Claim("FullName", "John Doe") };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));

        Assert.Equal("John Doe", user.FullName());
    }

    [Fact]
    public void FullName_ReturnsNull_WhenClaimMissing()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        Assert.Null(user.FullName());
    }
}
