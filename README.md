[![Build Status](https://dev.azure.com/nullloop/AFI%20Tech%20Exercise/_apis/build/status/CI%20%26%20CD?branchName=main)](https://dev.azure.com/nullloop/AFI%20Tech%20Exercise/_build/latest?definitionId=7&branchName=main)

## Tech Stack

* .NET Core 3.1
* xUnit
* EntityFramework Core
* FluentValidation
* Swashbuckle

## Run tests

From the root of the repo simply run:

```
dotnet test
```

All being well, you should see 
```
Passed!  - Failed:     0, Passed:    48, Skipped:     0, Total:    48, Duration: 2 s - AFIExercise.Tests.dll (netcoreapp3.1)
```

## Run API

From the root of the repo simply run:

```
dotnet run --project AFIExercise.API\AFIExercise.API.csproj
```

Once the host has started navigate to [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

## Projects

* `AFIExercise.API` - ASPNET Core Web API Host
* `AFIExercise.Services` - Registration validation & database submission
* `AFIExercise.Data` - Database access through Unit Of Work pattern
* `tests\AFIExercise.Tests` - Unit and integration tests

## Other files

* `azure-pipelines.yml` - YAML build script for Azure DevOps build pipeline. Builds, tests and deploys web app to Azure on Linux host.

## Databases

Local development and test makes use of LocalDB, which should be automatically created as required. Deployed Azure web app and Azure DevOps pipeline tests make use of Azure SQL Server instance.

## Links

Azure DevOps pipeline automatically deploys successful builds to `https://afi-exercise-web-api.azurewebsites.net`:

* [Swagger UI](https://afi-exercise-web-api.azurewebsites.net/swagger/index.html)
* [Swagger JSON](https://afi-exercise-web-api.azurewebsites.net/swagger/v1/swagger.json)

Azure Devops pipeline build results available at:

* [CI & CD pipeline](https://dev.azure.com/nullloop/AFI%20Tech%20Exercise/_build?definitionId=7)