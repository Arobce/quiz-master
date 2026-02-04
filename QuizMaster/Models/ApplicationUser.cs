using Microsoft.AspNetCore.Identity;

namespace QuizMaster.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
}