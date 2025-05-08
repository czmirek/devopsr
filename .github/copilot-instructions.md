# Devopsr Architecture Specification

## CLI Project
- All types and services must be registered in a dependency injection containerin the Devopsr.Lib project.

## Layers
- The CLI is the UI layer.
- Lib is the Data layer and the business logic layer
- Data layer is implemented in the Repositories directory.
- Business logic is implemented in Services directory.

## Business logic services
- All services must be invokable only via interfaces.
- All interface methods must be asynchronous. Do not use the Async suffix.
- All interface methods must accept a request model as input and return a response model as output wrapped in a Result from FluentResults.
- If Result.Fail is invoked, do not return the response model. Instead, return a relevant error code which should be implemented in a custom public static class called ErrorCodes.

## Repositories
- Repositories should accept and return the service models.
- Repositories do NOT use FluentResults and try/catch blocks.
- Repository methods always assume they are used correctly in the code. Business logic is responsible for using them correctly.
- Repository methods that write data should first map the service models into corresponding repository models and then serialize and save these models.
- Repository methods that read data should first deserialize into into corresponding repository models and then map them into service models.
- Mappers and repository models must be in the Repositories directory.

## Namespaces
- Namespaces must strictly follow the folder structure of the project.

## Input and Output Models of services
- All input (request) and output (response) models must be immutable.
- All models must be declared as sealed classes.
- All properties must be required, with only getters and initializers (no setters).
- Models should not contain constructors.
- Models must not expose mutable collections or allow mutation after construction.

## Types
- Use DateTimeOffset instead of DateTime
- Use TimeProvider instead of DateTime.Now
- Use LocalNow instead of UtcNow

## File Structure
- Each type (class, interface, struct, enum, etc.) must be declared in its own file. Do not declare multiple types in a single file.