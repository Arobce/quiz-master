using Microsoft.AspNetCore.Identity;

namespace QuizMaster.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}