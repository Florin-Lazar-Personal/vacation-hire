# Solution Architecture
Vacation Hire will be developed using a microservice architecture:
![SolutionArchitecture](/img/VacationHire-Component-Diagram-v1.svg)


## Bounded contexts

### (1) Authentication and Authorization
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


### (2) Asset management
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


### (3) Pricing definition
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


### (4) Customer management
> **Responsibilities:**
> - Manages customer data and associated details by enabling CRUD operations for customers.
> 
> **Associated microservice:**
> - __Customers API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.


### (5) Rental management
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


### (6) Invoicing
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


### (7) Payment processing
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


### (8) Exchange rates for price conversions
> **Responsibilities:**
> - Allows obtaining exchange rates between currencies by integrating with an external exchange rate service. The integration must be pluggable, so that the payment service integration can be switched from one provider to another, without affecting the service contract.
> - Keeps track of the currency exchanges performed, their timestamp and exchange rate provider for traceability.
>
> **Associated microservice:**
> - __Exchange Rate API__: developed in .NET Core using ASP.NET Core API. Data persistence: SQL Server. ORM: Entity Framework Core.
> 
> **Depends on:**
> - __Identity API__: for access token validation.


## Supporting services
Special consideration must be paid to dependencies between microservices, especially in a Kubernetes cluster deployment model, where PODs readiness and liveness may depend on the fact that a dependent service is available or not.
In such cases, a service becoming unavailable may lead to a cascading effect of other services becoming unavailable as well, hurting the platform availability/usability.

Circular (or bi-directional) dependencies also raise issues during (re)deployment, when one service in order to be redeployed will trigger a dependent service to become unhealthy, but also cannot start properly because of depending on the same service which it made unavailable during the redeploy.

In order to address such possible scenarios, certain functionalities need to be developed in such a manner that hard/synchronous direct dependencies are avoided and the platform relies rather on async/weak dependencies via events and messages.

In order to facilitate a simple, yet with a clear semantic communication model we propose using the [NServiceBus](https://docs.particular.net/nservicebus/messaging/messages-events-commands) library with the following communication styles:
- __Integration events__: are raised by the microservice that undergoes a certain state change, in order to notify any potential interested listeners. 
  - An integration event can be published only from within the service that defines it (event semantics)
  - An integration event can have 0 ... N listeners (either handlers inside the microservice that raised the event, either inside other microservices that are interested in reacting to that event)
  - If Service A publishes an event and Service B listens to that event, this introduces a weak depenency: Service B (listener) => depends-on => Service A (publisher)
- __Commands__: are accepted by the microservice that knows how to react to it
  - A command can have a single logical owner (the service that defines it; it must also implement the handling logic for it)
  - A command can be sent either from within the service that defines it, or from within another microservice.
  - If Service A sends a command to Service B, this introduces a weak depenency: Service A (command sender) => depends-on => Service B (command owner)

NServiceBus also offers benefits from operational perspective:
- it abstracts the transport layer, hence we can change easily the underlying messaging technology (e.g.: RabbitMQ or Azure Service Bus)
- it automatically configures the transport layer topology (e.g: automatically create exchanges and queues)
- it provides out-of-the box resilience strategies (message processing re-queue & retry in case of errors, supporting both immediate retries and delayed retries as well as dead letter queue(s)).


To minimize coupling between microservices and also to support automatic synchronization of updates between microservices, the following support services will be added to the platform:

### (1) Asset Attribute Values Pricing Synchronization Service
> **Responsibilities:**
> - Listens to asset-related events raised by __Asset Management API__, extracts (asset attribute, attribute value) pairs and synchronizes them with __Pricing API__ so they are available to be used for pricing rules definitions.
> - Allows a clearer separation of concerns between asset management (who doesn't need to know that there is such concept as pricing rules) and pricing definition (which doesn't need to know how to extract asset attributes that may influence pricing).
> - Allows avoiding a direct dependency between __Pricing API__ and __Asset Administration API__, allowing for e.g. __Pricing API__ to evolve over time and support pricing rules based also on other attribute kinds (not only asset-related).
> - Allows minimizing deployment depenendencies between __Pricing API__ and __Asset Administration API__:
>   - For e.g. if __Asset Administration API__ becomes unhealthy, or is redeployed, the effect doesn't cascade to __Pricing API__ and system is still able to operate with existing pricing rules.
>   - If __Pricing API__ becomes unhealthy, or is redeployed, any integration events published by __Asset Administration API__ in the meanwhile, will be eventually re-delivered after service comes back online, due __Asset Attribute Values Pricing Synchronization Service__ resilience strategies.
>
> **Associated microservice:**
> - __Asset Attribute Values Pricing Synchronization Service__: developed in .NET Core using NServiceBus.
> 
> **Depends on:**
> - __Asset Administration API__: because listens to asset-related events.
> - __Pricing API__: sends commands to update the set of pricing attributes that are available to be used when defining pricin policies.


### (2) Rented Asset Status Synchronization Service
> **Responsibilities:**
> - Listens to rental-related events raised by __Rental API__, extracts the rented asset and synchronizes asset status (availability) with __Asset Administration API__ so the asset status is updated in nearly real time.
> - Allows a clearer separation of concerns between asset management (who doesn't need to know that there is such concept as rental lifecycle) and rentals (which doesn't need to know about the need to update asset availability).
> - Allows avoiding a cyclic dependency between __Rental API__ and __Asset Administration API__
>
> **Associated microservice:**
> - __Rented Asset Status Synchronization Service__: developed in .NET Core using NServiceBus.
> 
> **Depends on:**
> - __Rental API__: because listens to rental lifecycle related events.
> - __Asset Administration API__: sends commands to update the asset availability.


### (3) Invoice Payment Synchronization Service
> **Responsibilities:**
> - Listens to payment-related events raised by __Payment API__, extracts the corresponding invoice and synchronizes invoice status with __Invoicing API__ so the invoice status is updated when payment is made.
> - Allows a clearer separation of concerns between payment management (who doesn't need to know that there is such concept as invoice lifecycle) and invoicing (which doesn't need to know about the details of payment processing).
> - Allows avoiding a cyclic dependency between __Payment API__ and __Invoicing API__
>
> **Associated microservice:**
> - __Invoice Payment Synchronization Service__: developed in .NET Core using NServiceBus.
> 
> **Depends on:**
> - __Payment API__: because listens to payment transactions approval related events.
> - __Invoicing API__: sends commands to update the invoice status.


## Dependency Diagram
![DependencyDiagram](/img/VacationHire-Dependency-Diagram-v1.svg)


## Other aspects
Other aspects to be considered for development:
- Services will expose healthchecks that can be used for Kubernetes startup/liveness/readiness probe (see also: https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-6-adding-health-checks-with-liveness-readiness-and-startup-probes/)
- Where synchronous HTTP calls are used for inter-service communication, resilience strategies must be configured (retry policies, circuit breaker). [Polly](https://github.com/App-vNext/Polly) library is a good fit for such policy definitions.
- When a service publishes integration events in order to signal state change, the [Transactional Outbox Pattern](https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-6-adding-health-checks-with-liveness-readiness-and-startup-probes/) will be used to ensure consistency between state update and event publish. [NServiceBus](https://docs.particular.net/nservicebus/outbox/) comes with a robust implementation, allowing usage of multiple database types for the pattern implementation.