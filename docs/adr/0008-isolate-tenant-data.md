# 8. Isolate tenant data

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)

## Context

In a multi-tenant scenario, as per the project brief, I must ensure complete data isolation between different tenants to prevent data leakage. I need to decide on an approach that will allow this separation of concerns to be brought about.

## Decision

I have decided to implement tenant isolation by using ID-based filtering at the data access level, automatically applying tenant filters to all queries.

**Alternatives considered:**
- Separate databases per tenant: High operational overhead
- Database-level security: Less flexible and harder to maintain
- Application-level checks only: Prone to human error

## Consequences

### Positive
- Automatic enforcement of data boundaries
- Reduced risk of accidental data exposure
- Consistent isolation strategy across all data access
- Easier to verify

### Negative
- Additional complexity in data access layer
- Performance overhead from additional filtering
- Requires extra testing to ensure isolation works correctly