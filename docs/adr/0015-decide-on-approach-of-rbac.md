# 15. Decide on approach of RBAC

Date: 2025-11-17

## Status

Accepted

## Context

I need to decide on an approach to implement the system's authorisation, Role Based Access Control (RBAC). The system requires restricted access to API endpoints and pages based on a user's role so that only authorised users can perform specific operations.

## Decision

I intend to implement RBAC using role based claims inside the JWT tokens introduced as part of the system's authentication approach, enforced through C#'s built in Authorize attribute. Each user in the system will have a role e.g Consumer or Help Desk Agent and the system will include a role claim when a user logs in. The API will then restrict access to certaim routes based on the user's role.

**Alternatives Considered:**
- Database driven permissions - requires extra overhead to manage extra tables, as well as being less secure and more resource intensive due to extra database lookups 

## Consequences

### Positives

- Lots of documentation to support this approach
- Makes use of built-in systems
- No additional database tables required

### Negatives

- Changing role permissions requires code changes and redeployment
- Not as granular for specific attribute based permissions