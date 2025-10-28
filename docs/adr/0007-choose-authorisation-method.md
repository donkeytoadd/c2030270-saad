# 7. Choose authorisation method

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)


## Context

We need to control what actions users can perform based on their roles within the system, ensuring users only access appropriate functionality.

## Decision

Implement Role-Based Access Control using ASP.NET Core's authorization policies and custom role requirements.

**Alternatives considered:**
- Claim-based authorization: Less structured for role management
- Attribute-based access control: Overly complex for our needs
- No authorization: Unacceptable security risk

## Consequences

### Positive
- Clear, manageable permission structure
- Easy to audit and modify user capabilities
- Integrates well with ASP.NET Core Identity
- Supports principle of least privilege

### Negative
- Role explosion potential if too granular
- Additional complexity in authorization checks
- Requires careful planning of role hierarchy