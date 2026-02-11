using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Models;

namespace QuizMaster.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<MCQOption> MCQOptions { get; set; }
    
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentAnswer>()
            .HasOne(sa => sa.Question)
            .WithMany()
            .HasForeignKey(sa => sa.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(qa => qa.Quiz)
            .WithMany()
            .HasForeignKey(qa => qa.QuizId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
