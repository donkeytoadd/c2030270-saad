# 11. Use MSSQL Database

Date: 2025-11-09

## Status

Accepted

## Context

I need to select a database management system based on using SQL as the query language. It needs to be efficient and easily usable, as well as aligning with my current experience with database systems.

## Decision

I have chosen to use Microsoft SQL Server as my choice for handling SQL queries and database management, as it provides a well maintained and official system that is easy to use.

**Alternatives considered:**

- MySQL - Open source and lightweight system, but offers less tight integraation with .NET
- PostgreSQL - Slightly more complex to learn and configure for this project
- MongoDB - NoSQL alternative that offers flexibility, but less suitable for relational and transactional data requirements for this project

## Consequences

### Positives

- Able to be used on Windows and MacOS
- Very well documented, both officially and with tutorials
- Worked with it extensively on my placement experience
- Able to work with .NET systems very easily
- Provides automatic backups
- Offers built in security tools (RLS, TDE, encryption etc)

### Negatives

- Requires a licence for full, long-term enteprise use
- Heavier resource usage
- Platform specific features may not work as well on non-Windows machines
