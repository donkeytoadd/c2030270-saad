# 4. Testing Framework Selection

## Context

I need to select a testing strategy and tooling for the backend that provides a reliable form of unit testing, mocking capabilities and that aligns with my current experience with testing frameworks. It should be maintainable, have good support, and be appropriate for a solo project.

## Decision

I will use **xUnit** as the primary testing framework with **Moq** for mocking dependencies and **AutoMocker** to simplify test setup and dependency injection.

**Alternatives considered:**

- **NUnit**: Mature framework but xUnit is more aligned with modern .NET development
- **MSTest**: Microsoft's default but less flexible than xUnit
- **Manual mocking**: Without Moq, would require extensive test setup code that brings about a lot of complex overhead
- **No AutoMocker**: Would result in more verbose test configuration

## Consequences

### Positive
- xUnit is modern, extensible, and well-supported in the .NET system
- Moq provides mocking capabilities for isolating units under test
- AutoMocker reduces test setup boilerplate and simplifies dependency management
- Strong support and extensive documentation
- Parallel test execution by default improves test performance

### Negative
- Additional dependencies to manage and update (Moq, AutoMocker)
- Potential over-engineering for simple test scenarios
- Requires discipline to maintain test quality and avoid test code duplication
