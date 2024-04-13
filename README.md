# Vacation Hire specs
Vacation hire is a platform that allows registration different asset types, registration of orders for rentals for these assets and generation of invoices for the offered services.
First of all an asset that can be rented must be registered within the system.
For example, for a vehicle the registration process should gather the following informations:
- the type of vehicle
- other characteristics

When the asset is rented the system should gather the following informations:
- when it is reserved
- customer data (e.g.: full name)
- customer contact (e.g.: phone number)

When the asset is returned:
- when it was returned
- if there is any damage
- is any specific characteristics missing (e.g.: gasoline)

All this data will then be used for invoicing.
The kind of vehicles that will be rented first are trucks, minivans and sedans. We already know that "Vacation Hire Inc" is planning to move into other business areas than just cars. 
Hence, in the future we expect to also rent out holiday cottages, hotel rooms and equipment like bikes and boats.
The program must be extendable to support the other types of rentals. The extension points / implementation methodology must be documented within the code and must be made clear which interfaces, or class inheritance, or other software patterns must be used.

Vacation Hire will allow payments with foreign currency. The latest exchange rates are to be used from an external api, e.g. [https://currencylayer.com](https://currencylayer.com/)

Other non-functional requirements:
- microservices architecture style
- use .NET for implementation of microservices
- use Docker to containerize the microservices
- the domain must be divded into the needed bounded contexts and associated microservices must be identified
- the platform must implement basic authentication and authorization mechanisms
- usage of RESTfull APIs for service communication
- show usage of cloud design patterns


