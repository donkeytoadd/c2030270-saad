# 9. Encrypt field level information

Date: 2025-10-27

## Status

Accepted

Supersedes [5. Choose Security Considerations](0005-choose-security-considerations.md)

## Context

Certain data fields (e.g. personal information, credentials) require additional protection at rest beyond regular database security. I need to decide on an approach that will securely encrypt any sensitive information and adhere to current industry standards.

## Decision

Implement field-level encryption for sensitive data using Entity Framework Core value converters and secure key management.

**Alternatives considered:**

- Full database encryption: High performance impact
- Application-level encryption only: Leaves data exposed in backups
- No additional encryption: Insufficient for sensitive data

## Consequences

### Positive
- Enhanced protection for sensitive data

- In depth defence against database compromises
- Selective encryption minimises performance impact on wider system
- Compliance with data protection regulations

### Negative

- Key management complexity
- Performance overhead for encrypted operations
- Complicates searching and indexing of encrypted fields
- Additional development and testing effort