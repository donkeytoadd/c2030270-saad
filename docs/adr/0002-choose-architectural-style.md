# 2. Choose Architectural Style

Date: 2025-10-01

## Status

Accepted

## Context

When building this proof-of-concept project, I need an architectural style that:

- Provides clear separation of concerns through distinct layers
- Follows SOLID principles for maintainable code
- Is straightforward to implement and understand
- Organizes code into logical folders (Business, Controllers, Data, Resources)
- Allows for clear demonstration of architectural decisions

## Decision

  I will implement **Layered Architecture** with the following structure:

```
.
└── CMS/
    ├── UI Project/
    │   ├── client-app/
    │   │   └── src
    │   └── www-root/
    │       └── Images, Assests, Static Files
    ├── Backend Project/
    │   ├── Controllers/
    │   │   └── API Controllers
    │   ├── Business/
    │   │   └── Getters, Setters, Helpers etc
    │   └── Data/
    │       └── Entities, Queries, Data Context
    └── Backend Unit Tests Project/
        ├── Controllers/
        │   └── Controller Unit Tests
        ├── Business/
        │   └── Unit Tests for Business Logic
        └── Data/
            └── Query Unit Tests
```

**SOLID Principles Application:**

- **Single Responsibility**: Each layer and class has one clear purpose
- **Open/Closed**: Layers are open for extension but closed for modification
- **Liskov Substitution**: Interfaces allow proper substitution
- **Interface Segregation**: Focused interfaces for each layer
- **Dependency Inversion**: Dependencies flow downward (Controllers → Business → Data)

Considered alternatives:

- **Clean Architecture**: More complex with dependency inversion
- **Microservices**: Overkill for a solo proof-of-concept, as well as being harder to work with both a user interface and backend simultaneously

## Consequences

### Positive

- Clear, intuitive folder structure
- Easier to understand and navigate for a solo project
- Straightforward implementation of SOLID principles
- Natural separation of concerns
- Faster development for proof-of-concept
- Easier to explain and demonstrate

### Negative

- Potential for tight coupling between layers
- Business layer depends directly on data layer
- Less flexible than Clean Architecture for future changes
- May require refactoring if project scales significantly