# Service-Toggler

## DataBase
To use the solution you must first select the database provider to use:
- In memory database
- SQL Server

In the **appsettings.json** file use the value **0** for in memory or **1** for sql server on the **ChosenDB** field.

If the provider chosen is sql server you have to change the field **ConnectionStrings/SqlServer** to the connection string you want.

## Organization
This solution has two main folders:
- Toggler Service (the main solution)
- Toggler Service Tests (the unit tests used on main solution)

## Endpoints
### Service Endpoint
**GET api/Service**: Lists all existing services

**GET api/Service/{identifier}/{version}**: Returns the service with the provided data

**GET api/Service/Toggles/{identifier}/{version}**: Lists all toggles connected with provided service

**POST api/Service**: Creation of a service (identifier and version set must be unique)

**POST api/Service/Bulk**: Creation of several services

### Toggle Endpoint
**POST api/Toggle**: Creation of the toggle and its additional data

This endpoint has as optinal fields:
- Value: boolean
- Include all services: boolean, if true creates the toggle service connection for all existing services
- Inclusions/Exclusions: list of services (version and identifier) to include or exclude the connection, cannot be used together and when include all services is true

**POST api/Toggle/Bulk**: Batch creation of several toggles according to release name

**PATCH api/Toggle/Services**: Add connection bewtween existing toggle and list of services 
