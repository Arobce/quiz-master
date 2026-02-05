using System.Security.Claims;

namespace QuizMaster.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? FullName(this ClaimsPrincipal user) => user.FindFirst("FullName")?.Value;
}