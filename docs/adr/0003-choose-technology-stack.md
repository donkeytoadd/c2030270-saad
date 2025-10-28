# 3. Technology Stack Selection

## Status

Accepted

## Context

I need to select appropriate technologies for a proof-of-concept that:

- Uses modern, industry-relevant technologies
- Separates frontend and backend concerns clearly
- Provides good development experience for solo development
- Has excellent documentation and learning resources
- Demonstrates full-stack development capabilities

## Decision

### Frontend

- **Angular 17+** - Modern, TypeScript-based framework with strong typing and excellent documentation
- **TypeScript** - Gives JavaScript strong typing
- **Angular Material** and **Bootstrap** - UI component library

Considered Alternatives:

- **Blazor** - Provides C# development for frontend, however I wanted to expand my learning to outside just the .NET frameworks
- **React** - Mainly for UI development as it only handles the "View" part in model-view-controller design patterns, in comparison to Angular offering the full MVC framework

### Backend

- **ASP.NET Core Web API** - Modern, performant REST API framework
- **.NET 8** - Latest LTS version with excellent performance
- **C#** - Modern language features and syntax
Considered Alternatives:

- **Java** - Modern, well supported programming language, however I have very little exposure to this language and as such, a lot of time at the start of the project would be dedicated to learning.

### Database & Data Access

- **SQL Server Management Studio** - Database management
- **Entity Framework Core** - For data access with LINQ support
- **SQL Server LocalDB** - For local development and testing

### API Communication

- **RESTful APIs** with JSON serialisation
- **Swagger/OpenAPI** for API documentation

### Development Tools

- **Visual Studio 2022** / **Rider** - IDE
- **Postman** - API testing
- **Git** - Version control

I decided on all of these options for my technology stack as they are the same technologies that I worked with on during my placment year, and as such I have the most knowledge and comfort within them. Additionally, they are all modern and have excellent documentation which will make it easy for any future developers to look at and understand even if they are not well-versed in any of the particular technologies.

## Consequences

### Positive

- I have worked with all of these technologies on placement, so learning curve is not as high 
- Angular frontend completely separate from .NET backend
- Both stacks widely used in enterprise
- Extensive libraries and packages available
- Excellent learning resources and support available for both
- Built-in testing frameworks for both frontend and backend
- Clean separation allows independent development

### Negative

- By choosing not to explore other languages, I may potentially be limiting myself on the knowledge I can gain.
- Possibly two separate codebases to maintain
- Need to run both Angular dev server and .NET API
- Two separate applications to deploy
- May need similar models in both frontend and backend