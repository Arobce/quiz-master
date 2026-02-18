# QuizMaster

A quiz management web application built with ASP.NET Core MVC where teachers create and manage quizzes, and students take them and view results.

## Inspiration

This project replicates the Nexus quiz system used at the University of Winnipeg. The major purpose was to practice writing well-structured, layered code and to learn Entity Framework Core in a real-world context.

## Features

- **Teacher dashboard** — Create quizzes, add MCQ and text questions, view student submissions and scores
- **Student experience** — Browse available quizzes, take quizzes, get auto-graded results for MCQs, review past submissions
- **Role-based auth** — ASP.NET Identity with Teacher and Student roles, seeded on startup
- **Auto-grading** — MCQ answers are graded automatically on submission

## Tech Stack

- **Framework:** ASP.NET Core MVC (.NET 10)
- **ORM:** Entity Framework Core 10
- **Database:** SQL Server 2022 (Docker)
- **Auth:** ASP.NET Core Identity

## Application Architecture

```
Controllers → Services (interfaces) → Repositories (interfaces) → ApplicationDbContext
```

All dependencies use interface-based DI registered as scoped services.

## AWS Architecture

<!-- TODO: Add architecture diagram -->

- **CI/CD:** AWS CodePipeline connected to GitHub — triggers on push
- **Build & Test:** AWS CodeBuild builds the application and runs tests
- **Hosting:** AWS Elastic Beanstalk serves the .NET app
- **Database:** SQL Server on Amazon RDS

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)

### Run

```bash
# Start SQL Server
docker compose up -d

# Apply migrations
dotnet ef database update --project QuizMaster

# Run the app (https://localhost:5001)
dotnet run --project QuizMaster/QuizMaster.csproj
```
