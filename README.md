
## Details
Built with .NET Core 3.1 and EF 3.1

Unit tests done with xUnit and MOQ

## Building and Running

Build the project

`dotnet build`

Run the tests

`dotnet test`

Using package manager update the database from the `AppointmentManager.Data` project context

`Update-Database`

Run the API

Change directory to `AppointmentManager.API` and do a `dotnet run`

## Postman Collections
There are some postman collections you can use for testing along with an environment import.

Import these all into Postman. They can be found in the `AppointmentManager.Services.Tests` folder

- Appointment Endpoints.postman_collection.json
- Development.postman_environment.json

## Default User
The Identity database is seeded with a default user in the system.

You can login with this user. Details are in the Postman collection

```json
{
	"username": "testpatient",
	"password": "P@ssw0rd!"
}
```
