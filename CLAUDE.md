# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
# Start SQL Server (required before running the app)
docker compose up -d

# Restore dependencies
dotnet restore QuizMaster/QuizMaster.csproj

# Build
dotnet build QuizMaster/QuizMaster.csproj

# Run (starts on HTTPS, default https://localhost:5001)
dotnet run --project QuizMaster/QuizMaster.csproj

# EF Core migrations
dotnet ef migrations add <MigrationName> --project QuizMaster
dotnet ef database update --project QuizMaster
```

There are no tests in this project yet.

## Architecture

ASP.NET Core MVC (.NET 10) app with layered architecture:

```
Controllers → Services (interfaces) → Repositories (interfaces) → ApplicationDbContext
```

All services and repositories use interface-based DI, registered as scoped in `Program.cs`.

**Database:** SQL Server 2022 via Docker (`compose.yaml`), EF Core with `IdentityDbContext<ApplicationUser>`. Connection string in `appsettings.json`. Roles ("Teacher", "Student") are seeded on startup via `Data/Seed/RoleSeeder.cs`.

### Key Layers

- **Models/** — Domain entities (`Quiz`, `Question`, `MCQOption`, `QuizAttempt`, `StudentAnswer`, `ApplicationUser` extending `IdentityUser` with `FullName`)
- **Models/ViewModel/** — View-specific models organized by feature: `Account/`, `Quiz/`, `Question/`, `Quiz/Student/`
- **Repositories/** — Data access with interfaces in `Repositories/Interfaces/`
- **Services/** — Business logic with interfaces in `Services/Interfaces/`
- **Controllers/** — 5 controllers: `Account`, `Home`, `Quiz` (Teacher), `Question` (Teacher), `StudentQuiz`
- **Data/** — `ApplicationDbContext` with FK configuration (NoAction deletes on `StudentAnswer→Question` and `QuizAttempt→Quiz`)

### Authorization Model

- **Teacher role:** `QuizController` and `QuestionController` are `[Authorize(Roles = "Teacher")]`
- **Student role:** `StudentQuizController` handles quiz-taking; ownership verified in `StudentQuizService`
- Custom `FullName` claim added at login/register, accessed via `Extensions/ClaimsPrincipalExtensions.cs`

### Question Types

Questions have a `Type` field: `"MCQ"` (with `MCQOption` children, auto-graded) or `"Text"` (with `SampleAnswer`, flagged `IsAiGraded` but not yet implemented).

## Commit Message Convention

Prefix commits with a tag: `feat:`, `add:`, `fix:`, etc. (see git log for examples).
