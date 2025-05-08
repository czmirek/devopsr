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
- Service methods must accept a request model as input and return either non generic fluent result or generic fluent result with the response model.
- Result.Fail must be returned only with an error code.
- Error codes are implemented in a custom public static class called ErrorCodes.
- Request and response models must be sealed immutable classes and all properties must be required with get and init.

## Repositories
- Repositories should accept and return the service models.
- Repositories do not use FluentResults and try/catch blocks.
- Repository models are always fully mutable and used only in repositories.
- Repositories map between service models and repository models.
- Repository models are used to serialize to storage or deserialize from storage.

## General
- Do not use the Async suffix.
- Namespaces must strictly follow the folder structure of the project.
- Use DateTimeOffset instead of DateTime
- Use TimeProvider instead of DateTime.Now
- Use LocalNow instead of UtcNow
- Do not use regions
- Each type must be declared in its own file except nested private types.
- Do not declare multiple types in a single file.