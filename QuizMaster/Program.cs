using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Data.Seed;
using QuizMaster.Models;
using QuizMaster.Repositories;
using QuizMaster.Repositories.Interfaces;
using QuizMaster.Services;
using QuizMaster.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services (DI) — MUST be before Build
// =======================

builder.Services.AddControllersWithViews();


// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity (WITH ROLES)
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<IStudentQuizService, StudentQuizService>();
builder.Services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();


// Auth cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// =======================
// Middleware
// =======================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Security headers middleware (CSP + Clickjacking protection)
app.Use(async (context, next) =>
{
    // Clickjacking protection
    context.Response.Headers["X-Frame-Options"] = "DENY";

    // Basic hardening headers
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

    // CSP (includes modern clickjacking protection via frame-ancestors)
    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "frame-ancestors 'none'; " +
        "object-src 'none'; " +
        "base-uri 'self';";

    await next();
});


app.UseAuthentication();
app.UseAuthorization();

// =======================
// Creating Migrations & Updating DB (AFTER app build)
// =======================

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// =======================
// Seeding (AFTER app build)
// =======================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}




// =======================
// Routing
// =======================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
