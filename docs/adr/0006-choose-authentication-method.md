# 6. Choose authentication method

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)


## Context

I need a secure, scalable authentication mechanism that works well with REST APIs and that can be easily integrated with an Angular frontend.

## Decision

I have decided to use JSON Web Tokens (JWT) for the system's user authentication.

**Alternatives considered:**
- Session-based authentication: Requires server-side state management and can often perform slower, especially when it needs to communicate with external systems or databases
- OAuth/OpenID Connect: Overly complex for the application scope

## Consequences

### Positive
- Stateless authentication makes it suitable for systems with a large growing number of users, which applies to the project brief
- Self contained tokens reduce database lookups
- Standardised approach with strong support

### Negative
- Larger token size compared to session identifiers
- Need to manage token expiration and refresh strategies