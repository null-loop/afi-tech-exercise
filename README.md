[![Build Status](https://dev.azure.com/nullloop/AFI%20Tech%20Exercise/_apis/build/status/CI%20%26%20CD?branchName=main)](https://dev.azure.com/nullloop/AFI%20Tech%20Exercise/_build/latest?definitionId=7&branchName=main)

# AFI Tech Exercise

## Tech Stack

* .NET Core 3.1
* xUnit
* EntityFramework Core
* FluentValidation
* Swashbuckle

## Build & Test

Clone repository and simply run:

```
dotnet build
```
or
```
dotnet test
```

## Projects

* `AFIExercise.API` - ASPNET Core Web API Host
* `AFIExercise.Services` - Registration validation & database submission
* `AFIExercise.Data` - Database access through Unit Of Work pattern
* `tests\AFIExercise.Tests` - Unit and integration tests

## Other files

* `azure-pipelines.yml` - YAML build script for Azure DevOps build pipeline. Builds, tests and deploys web app to Azure on Linux host.

## Databases

Local development and test makes use of LocalDB. Deployed Azure web app and Azure DevOps pipeline tests make use of Azure SQL Server instance.

## Deployed instance

Azure DevOps pipeline automatically deploys successful builds to `https://afi-exercise-web-api.azurewebsites.net`:

* [Swagger UI](https://afi-exercise-web-api.azurewebsites.net/swagger/index.html)
* [Swagger JSON](https://afi-exercise-web-api.azurewebsites.net/swagger/v1/swagger.json)