# 3. Technology Stack for Layered Architecture

## Status

Accepted

## Context

Need to select technologies that work well with Layered Architecture and support SOLID principles implementation in a C# university project.

## Decision

### Architecture & Framework
- **Layered Architecture (N-Tier)** with clear separation
- **ASP.NET Core Web API** for the presentation layer
- **.NET 8** as the runtime

### Layer Implementation
- **Controllers Layer**: ASP.NET Core Controllers with minimal logic
- **Business Layer**: Service classes implementing business rules
- **Data Layer**: Entity Framework Core
- **Resources Layer**: DTOs, ViewModels, configuration objects

### Data Access
- **Entity Framework Core** as ORM
- **SQL Server LocalDB** for local development

### Supporting Technologies
- **AutoMapper** for object-object mapping
- **FluentValidation** for input validation
- **xUnit & Moq** for unit testing

## Consequences

### Positive
- Clear technology alignment with architectural style
- EF Core Repository pattern supports SOLID principles
- Easy to demonstrate layer separation
- Good support for testing each layer independently

### Negative
- Repository pattern with EF Core can be considered an anti-pattern by some
- Additional abstraction may be unnecessary for simple CRUD