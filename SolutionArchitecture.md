# Solution Architecture
Vacation Hire will be developed using a microservice architecture:
![SolutionArchitecture](/img/VacationHire-Component-Diagram-v1.svg)

## Bounded contexts

### Authentication and Authorization
**Responsibilities:**
- Defines and manages the platform authorization setup, by defining protected API Resources and associated scopes
- Defines the set of supported OAuth clients (apps) that can integrate with the platform and their allowed API usage
- Defines the role based access control policies
- Manages the users database
- Enables user login
- Enables access tokens issuance, validation, renewal and revokation by using OAuth 2.0 and OIDC protocols

**Associated microservice:**
- __Identity API__: developed in .NET Core using ASP.NET Core API, ASP.NET Identity and Duende Identity Server for OAuth/OIDC server-side protocol implementation. Data persistence: SQL Server. ORM: Entity Framework Core.

**Depends on:**
- None

### Asset management
**Responsibilities:**
- Management of asset categories (types) by enabling CRUD operations for asset categories (types)
- Management of assets: by enabling CRUD operations for assets
- Management of asset attributes: by allowing to define a set of attributes and their values that will be associated to a given asset

**Associated microservice:**
- __Identity API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.

**Depends on:**
- __Identity API__: for access token validation.

## Dependency Diagram
![DependencyDiagram](/img/VacationHire-Dependency-Diagram-v1.svg)

