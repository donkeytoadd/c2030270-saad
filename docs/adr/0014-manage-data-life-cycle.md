# 14. Manage data life cycle

Date: 2025-11-11

## Status

Accepted

## Context

One of the system's requirements is to be able to store multi-tenant data, including personal info as well as transactional info via the complaints. I need to decide on how to manage this data in a efficient, compliant and well-structured way. It needs to ensure that data is created, used, archived and deleted securely as to mantain compliance with GDPR and other data protection regulations.

## Decision

A data strategy will be implemented to ensure all complaint, tenant and personal info follows a controlled path. It will consist of the following key stages:

1. Data Creation

- Data will be created within the SQL Server database and accessed through the .NET API
- Only authorised users with valid JWT tokens and correct role permissions can create or modify data

2. Active Storage

- Data will remain in the database for active use, protected by SQL Server's built in Transparent Data Encryption (TDE)

3. Data Backup and Retention

- Daily incremental and weekly full database backups
- Data will only be retained in accordance to GDPR and other regulatory bodies

4. Deletion/Archival
 
- After a defined retention period, inactive accounts or closed complaint data will be closed with this status change being reflected in the database
- Request/response metadata will be retained for auditing purposes
- Relevant data will be deleted at user request

## Consequences

### Positives

- Ensures GDPR and other regulation compliance by enforcing data minimisation and retention
- Enhances security by encrypting data in use, at rest and in transit
- Enables database recovery

### Negatives

- Additional complexity by having to manage multiple data states
- Monitoring required to ensure data lifecycle is followed
