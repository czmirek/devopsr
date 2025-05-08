# Devopsr Architecture Specification

## Layers
- The CLI is the UI layer.
- Lib is the Data layer and the business logic layer
- Data layer is implemented in the Repositories directory.
- Business logic is implemented in Handlers directory.

## Dependency injection
- All types and services must be registered in a dependency injection container in the Devopsr.Lib project.

## Business logic Handlers
- All business logic is contained in Handlers directory containing MediatR handlers.
- Service methods must accept a request model as input and return either non generic result or generic result with the response model.
- Result.Fail must be returned only with an error code.
- Error codes are implemented in a custom public static class called ErrorCodes.

## Repositories
- Repositories should accept and return the service models.
- Repositories do not use FluentResults and try/catch blocks.
- Repository models are always fully mutable.
- Repository methods assume they are used correctly from the business logic. 
- Repository methods that write data should first map the service models into corresponding repository models and then serialize and save these models.
- Repository methods that read data should first deserialize into into corresponding repository models and then map them into service models.
- Mappers and repository models must be in the Repositories directory.

## Namespaces
- Namespaces must strictly follow the folder structure of the project.

## Async
- Do not use the Async suffix.

## Request and response models.
- Request and response models must be always immutable.
- Request and response models must be suffixed with Request or Response and have all properties required with getters and initializers.

## Service models
- Service models must be suffixed with ServiceModel.
- Service models can be mutable.

## General
- Use DateTimeOffset instead of DateTime
- Use TimeProvider instead of DateTime.Now
- Use LocalNow instead of UtcNow
- Do not use regions
- Each type must be declared in its own file except nested private types.
- Do not declare multiple types in a single file.