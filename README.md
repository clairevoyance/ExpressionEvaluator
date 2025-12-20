# ExpressionEvaluator API

This is a small ASP.NET Core Web API.

The service evaluates arithmetic expressions passed as strings, stores each request and result in a SQLite database, and exposes a GET endpoint to retrieve previously evaluated expressions by result.

---

## Tech Stack

- ASP.NET Core (.NET 8)
- Entity Framework Core
- SQLite (file-based database)
- Swagger for local testing

---

## How to Run

### Prerequisites
- .NET SDK 8.0

### Steps

```bash
cd ExpressionEvaluator.Api
dotnet restore
dotnet build
dotnet run
