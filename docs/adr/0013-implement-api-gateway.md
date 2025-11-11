# 13. Implement API Gateway

Date: 2025-11-09

## Status

Accepted

## Context

This system is designed to support multiple, large scale tenants and potentially a high upstream of users. I need to decide on a way to handle high volumes of traffic whilst also ensuring data security and scalability. It also needs to be decided on with the possibility of the backend being moved from a monolithic design with just one API, to multiple APIs.  

## Decision

I intend to implement the API Gateway through the use of AWS' API Gateway which provides a fully managed solution that integrates very well with other AWS services that will be used throughout the system. It will route all API requests through a single entry point before reaching the relavant backend functionality.

**Azure API Management** - Works well with .NET but system is more involved with AWS systems
**Direct API Exposure** - Simplifies setup but at the cost of limited monitoring, no rate limiting

## Consequences

### Positives

- Allows for extra security
- Allows requests and traffic to be logged and monitored
- Acts as a central entry point for all traffic

### Negatives

- Additional configuration/setup for mapping endpoints
- Cost associated with request/traffic amount