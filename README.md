# Devopser

Devopser is aimed to be a swiss knife tool for people employed in a DevOps role.

It is a true IAC in the sense that it helps to organize other DevOps tools and scripts into a central overview.

## Brainstorming about the purpose

- The application is a tool for real people so it must have a UI.

- But it cannot be an app with a backend, frontend and a database. This application aims to solve the problem of being a **root of all DevOps**, the **true IAC**. It cannot be the root of all DevOps if you still need to figure out where to store the backend config, frontend config, connection strings, etc.

- It must be a native application with UI and all the data the DevOps operator is working with must be stored in a **single file**.

- The application is built in a way that the DevOps operator does not need more files to maintain multiple applications,  projects, companies or settings. However using multiple files might be advantageous for different contexts e.g. one file for my own projects, another file for a client etc.

- The data that the DevOps operator works may need to be read by other applications. In that case the single file can be saved as JSON or XML.

- If the DevOps operator decides to store secrets into the file (instead of just references to the secrets) then he has an option to **encrypt the file** with a password known only to him. The aim of this tool is to be as simple to use as possible.

## Brainstorming about the UI

- The application has a UI similar to KeePass. You can create a new database or open an existing database. That database is always just a single file with a cool ".devopser" extension.

- When a database is opened, it is loaded into memory. Any changes made to that file must be explicitly saved with CTRL+S. The DevOps operator can configure the database to auto save in specific intervals since it is assumed the operator will update the file a lot. If the file is inside a git repository then it can be configured to commit and push after each save.

- Similar to keepass, everything important is organized into a tree. The tree has a single root and nodes nestable to any depth.

- The root has configurable properties related to the database as a whole (e.g. database metadata) but it is a node too.

- The node can represent anything required for DevOps operations: company, project, application, environment, deployment etc. The nomenclature is important and often completely different even between two projects in a single company. Sometimes projects define applications. Sometimes applications are used in projects.

## Brainstorming about nodes

- Nodes have following **core properties**:
  - Name
  - Description
  - Icon shown in the tree (like in KeePass, I really like how that thing is created)
- Nodes are windows which have tabs, each tab has a type and a node can have only a single tab of a specific type.
- New node has no tab. To use a tab in the node you need to insert it there.
- Each tab type has a specific functionality and goals.
- There are following tab types:
  - Properties
  - Configuration
  - Scripts
  - Log
  - Documentation

## Brainstorming about tab types

- **Properties** tab contains a simple key-value list of whatever semantically related to the node and its descendants. For example if the node is representing an "Application" then its property may be a "tech stack" with a value "ASP.NET website written in .NET 9". It is extension to the **core properties**.
  - The node can force all of its children to:
    - require a properties tab
    - inherit all keys or specific keys and forbid to change its value
    - allow all keys or specific keys to have a value
    - require all keys or specific keys to always have a non empty value
  - Property keys must be unique

- **Configuration** is also a key-value list but semantically related to the real world usage of the node, not to the node itself. For example if the node is "Application" then the configuration might be something like "DeploymentURL", "Server Name" etc.
  - A single configuration value can be marked as secret. You cannot do this with properties.
  - There is different semantics from properties:
    - Configuration cannot be inherited. It's always related to the node only.
    - Configuration keys have unique IDs automatically assigned when created. Therefore you can have multiple keys with same name.
    - Configuration can be **referenced** in another configuration (or anywhere else in Devopser) either directly (by its ID) or by traversing the tree. For example you can define configuration like this: `BACKEND_URL = https://myapp${{parent.configuration.ENV}}.azurewebsites.net`. This value *resolves* into `https://myapptest.azurewebsites.net` everytime it's read by another mechanisms in the application. Referencing by ID could look like this: `https://myapp${{cref(12345)}}.azurewebsites.net`.

## Brainstorming about secrets

Configuration values can be marked as secrets which has an effect to some other tabs. Secrets are always masked with `***` if shown anywhere in the application and can be unmasked with a mouse click if required.

A configuration value can also be marked as a secret reference but .......todo