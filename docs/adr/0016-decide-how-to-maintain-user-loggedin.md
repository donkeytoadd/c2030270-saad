# 16. Decide how to maintain user logged in

Date: 2025-12-03

## Status

Accepted

## Context

Due to the nature of how I am generating JWT tokens when a user logs in, they will expire after a set period of time. This will become a pain point since if a user is logged in for x time and then the JWT token expires, they will be logged out. This will decrease user productivity and make the user experience disruptive. I need to find a solution to deal with a user having their logged in status persistent,

## Decision

I will implement a dual token authentication approach consisting of:

- A Short Lived JWT access token
    - Contains user identity and their role
    - Expires quickly (~15 minutes)
    - Used for every authorised API request

- Long Lived refresh tokens
    - Stored securely in a database table
    - Mapped to a specfic user
    - Can be revoked or rotated
    - Used to obtain a new JWT access token when it expires

When the access token expires, the frontend will automatically call a refresh endpoint with the refresh token in the request body. This will then be validated by the backend and once done, it will issue a new access token and a rotated refresh token. 

All of this should maintain session continuity without keeping permenant tokens in use.

**Alternatives considered:**

- Long-lived JWT access tokens: Simplest implementation and doesnt require storage externally, however they pose a high security risk 

## Consequences

### Positive

- Users stay logged in seamlessly without having to re-authenticate or log back in
- Access tokens remain short-lived, reducing risk if stolen
- Supports multiple roles through the same login page
- Allows login from multiple devices safely

### Negative

- High intital complexity to setup JWT and refresh tokens
- Requires additional database queries to search for refresh tokens
- More code required in backend and frontend to maintain token lifecyle