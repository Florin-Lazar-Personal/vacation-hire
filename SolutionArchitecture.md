# Solution Architecture
Vacation Hire will be developed using a microservice architecture:
![SolutionArchitecture](/img/VacationHire-Component-Diagram-v1.svg)


## Bounded contexts

### Authentication and Authorization
> **Responsibilities:**
> - Defines and manages the platform authorization setup, by defining protected API Resources and associated scopes
> - Defines the set of supported OAuth clients (apps) that can integrate with the platform and their allowed API usage
> - Defines the role based access control policies
> - Manages the users database
> - Enables user login
> - Enables access tokens issuance, validation, renewal and revokation by using OAuth 2.0 and OIDC protocols
> 
> **Associated microservice:**
> - __Identity API__: developed in .NET Core using ASP.NET Core API, ASP.NET Identity and Duende Identity Server for OAuth/OIDC server-side protocol implementation. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - None

### Asset management
> **Responsibilities:**
> - Management of asset categories (types) by enabling CRUD operations for asset categories (types)
> - Management of assets: by enabling CRUD operations for assets
> - Management of asset attributes: by allowing to define a set of attributes and their values that will be associated to a given asset
> 
> **Associated microservice:**
> - __Asset Administration API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.

### Pricing definition
> **Responsibilities:**
> - Implements a flexible / rule based pricing model that can be applied for asset rental, but also for damages associated with the asset rental, or missing characteristics noticed upon asset return.
> - Allows management of pricing categories: by implementing a set of CRUD operations for pricing categories.
>   - For e.g. pricing categories can be: "Car rental", "Car damage", "Car missing characteristics", etc.
> - Allows management of pricing attributes: by implementing a set of CRUD operations for pricing attributes. Asset attributes will be fed into the pricing attributes stores by means of automatic external synchronization (based on integration events raised by the __Asset Administration API__).
>   - For e.g. pricing attributes can be: "body-type=SUV", "broken-windshield=YES", etc. 
> - Allows management of pricing rules: by implementing a set of CRUD operations for pricing rules.
>   - For e.g. a pricing rule can be defined as follows: For pricing category "Car rental" for attribute "body-type=SUV" the price is 50 EUR for the unit of measure 1 Day.
> - Allows management of supporting data-structures, such as: unit of measures and conversions between units of measures of the same type
> - Implements the ability to perform pricing calculations for a given pricing rule if quantity and apropriate unit of measure is specified
> - Supports lookup for candidate pricing rules that can be used when specifying a pricing category and a given attribute
> 
> **Associated microservice:**
> - __Pricing API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.

## Dependency Diagram
![DependencyDiagram](/img/VacationHire-Dependency-Diagram-v1.svg)

