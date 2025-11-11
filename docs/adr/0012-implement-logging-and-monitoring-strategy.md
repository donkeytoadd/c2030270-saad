# 12. Implement logging and monitoring strategy

Date: 2025-11-09

## Status

Accepted

## Context

This system will need to operate at a large scale with multiple tenants and their users operating at the same time, therefore visibility into the system's behaviour, performance and error handling is extremely necessary. I need to decide on some system(s) that iwill provide monitoring and logging for:

- Application errors and exceptions
- API request latency
- User activity
- System health

## Decision

I intend to implement monitoring through AWS CloudWatch as it provides a well put together system for collecting and visualising logs, metrics and alerts. This means that I can setup alerts for the API gateway as well as any recurring 4xx/5xx errors so I can notice any spikes or performance issues. Additionally it will integrate well with SNS, S3 for monitoring on those as well.

For logging of errors and exceptions, I intend to implement thorough error handling in the design of my code, and then log info, warnings and errors within classes using .NET's built in logging ILogger. 

**Alternatives Considered**

- Azure Monitor and Application Insights - Works very well with the way the system is designed, but AWS I have worked with more as well as being more thoroughly used within the system

## Consequences

### Positives

- AWS Cloudwatch provides real-time visibility into system performance
- Can trigger alerts for failed logins, traffic spikes or system failures
- Centralises logging for all layers
- Simplifies troubleshooting and debugging
- Supports compliance by maintaining detailed event logs

### Negatives

- CloudWatch incurs costs for log storage
- Requires configuration of log retention policies to avoid excessive storage
