# 10. Choose level of api exposure

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)


## Context

I should only be exposing endpoints that are actually required by the frontend in order to reduce the surface available for any malicious attacks. I need to decide on a certain degree of exposure to minimise the risks.

## Decision

Implement a minimal endpoint exposure strategy where we consciously design and expose only necessary API endpoints, avoiding generic CRUD endpoints when not needed.

**Alternatives considered:**
- Expose all potential endpoints: Maximum flexibility but maximum attack surface

## Consequences

### Positive
- Reduced attack surface
- Clearer API design
- Better alignment with frontend needs
- Easier to secure

### Negative
- More intentional API design required
- Potential need for more endpoints later