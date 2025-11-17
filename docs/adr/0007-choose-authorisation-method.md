# 7. Choose authorisation method

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)


## Context

I need to control what actions users can perform based on their roles within the system, ensuring users only access appropriate functionality.

## Decision

Implement Role-Based Access Control using ASP.NET Core's authorisation policies and custom role requirements.

**Alternatives considered:**
- Claim-based authorisation: Less structured for role management
- Attribute-based access control: Lots of overhead for setting up attributes and planning out how its done
- No authorisation: Unacceptable security risk

## Consequences

### Positive
- Clear, manageable permission structure
- Easy to audit and modify user capabilities
- Integrates well with .NET Core 
- Supports principle of least privilege

### Negative
- Role explosion potential if too granular
- Additional complexity in authorisation checks
- Requires careful planning of role hierarchy