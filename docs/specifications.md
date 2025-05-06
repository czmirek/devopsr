# Devopsr Architecture Specification

## CLI Project
- The CLI project must not contain any business logic.
- The CLI project must not create or instantiate any classes directly.
- All types and services must be registered in a dependency injection container, which is created and configured inside the Devopsr.Lib project.
The CLI project should only obtain a fully configured IServiceProvider (or equivalent) from the Lib project and use it to resolve required services.

## Services and Interfaces
- All functionality must be implemented in services, not in the CLI.
- All services must be invokable only via interfaces.
- All interface methods must be asynchronous. Do not use the Async suffix.
- All interface methods must accept a request model as input and return a response model as output.

## Namespaces
- Namespaces must strictly follow the folder structure of the project.

## Input and Output Models
- All input (request) and output (response) models must be immutable.
- All models must be declared as sealed classes.
- All properties must be required, with only getters and initializers (no setters).
- Models should not contain constructors.
- Models must not expose mutable collections or allow mutation after construction.


## File Structure
- Each type (class, interface, struct, enum, etc.) must be declared in its own file. Do not declare multiple types in a single file.