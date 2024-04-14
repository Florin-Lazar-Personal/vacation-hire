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
> - Enables user operations such as: login, MFA, forgot password, reset password, change password
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

### Rental management
> **Responsibilities:**
> - Handles the rental process including: creation of new rentals, specifying rented asset, handling rental lifecycle, observing damages and missing characteristics upon rental end.
> - Integrates with __Asset Administration API__ for asset validation upon rental creation.
> - Integrates with __Pricing API__ for allowing to specify the pricing rules that apply for the asset rental, or for the observed damages or missing characteristics.
> - Keeps an audit trail for the rental process (changes, events occured, etc)
>
> **Associated microservice:**
> - __Rental API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.
> - __Asset Administration API__: for asset validation.
> - __Pricing API__: for pricing rule associations with rented asset, damages or missing characteristics.

### Invoicing
> **Responsibilities:**
> - Allows generation of invoices for rental process.
> - Integrates with __Rental API__ for invoice generation based on the details of a rental.
> - Integrates with __Pricing API__ for the calculation of actual costs during the invoice generation.
> - Manages the invoice lifecycle, including knowing when an invoice is paid by means of external synchronization via integration events.
> - Keeps an audit trail for the invoice changes.
>
> **Associated microservice:**
> - __Invoicing API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.
> - __Rental API__: for getting the rental details upon invoice generation.
> - __Pricing API__: for pricing rule associations with rented asset, damages or missing characteristics.

### Payment processing
> **Responsibilities:**
> - Integrates with __Invoicing API__ for the payment initiation.
> - Allows online payments by integrating with an external payment service. The integration must be pluggable, so that the payment service integration can be switched from one provider to another, without affecting the service contract.
> - Exposes webhooks that can be used for getting notified about transactions approval status.
> - Keeps track of the transactions lifecycle.
> - Keeps an audit trail for the transaction status changes.
>
> **Associated microservice:**
> - __Payment API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.
> - __Invoicing API__: for the payment initiation.

### Exchange rates for price conversions
> **Responsibilities:**
> - Allows obtaining exchange rates between currencies by integrating with an external exchange rate service. The integration must be pluggable, so that the payment service integration can be switched from one provider to another, without affecting the service contract.
> - Keeps track of the currency exchanges performed, their timestamp and exchange rate provider for traceability.
>
> **Associated microservice:**
> - __Exchange Rate API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.


## Dependency Diagram
![DependencyDiagram](/img/VacationHire-Dependency-Diagram-v1.svg)

