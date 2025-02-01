# Devopsr

Devopsr is aimed to be a swiss knife tool for DevOps operators and programmers.

It is a true IAC in the sense that it helps to organize other DevOps tools and scripts into a single central place.

## Brainstorming about the purpose

- The application is a tool for real people so it must have a UI.

- It cannot be an app with a backend, frontend and a database. This application aims to solve the problem of being a **root of all DevOps**, the **true IAC**. It cannot be the root of all DevOps if you still need to figure out where to store the backend config, frontend config, connection strings, etc.

- It must be a native application with UI and all the data the DevOps operator is working with must be stored in a **single file**. It cannot be simpler than that.

- The application is built in a way that the DevOps operator does not need more files to maintain multiple applications, projects, companies or settings. However using multiple files might be advantageous for different contexts e.g. one file for my own projects, another file for a client etc.

- The data that the DevOps operator works may need to be read by other applications. In that case the single file can be saved as JSON or XML. The file structure follows a hierarchical format matching the UI tree structure, with each node containing its tabs and their content.

- If the DevOps operator decides to store secrets into the file (instead of just references to the secrets) then he has an option to **encrypt the file** with a password known only to him. The encryption uses industry standard AES-256 encryption with a key derived from the password using PBKDF2. The aim of this tool is to be as simple to use as possible while maintaining security.

- Devopsr aims to be a tool for individuals which might not fit all DevOps settings e.g. companies or corporations with strict role definitions.

- Devopsr does nothing by itself without user defined content. Devopsr is meant to call other tools like terraform, various clis, etc. 

- Devopsr is mainly IAC tool, not CI/CD. It can be used as one though, but there are better things that can be used as CI/CD where Devopsr only acts as a manual proxy tool.

- **Root of all DevOps** is the most important philosophy of this tool.

## Brainstorming about the UI

- The application has a UI similar to KeePass. You can create a new database or open an existing database. That database is always just a single file with a cool ".devopsr" extension.

- When a database is opened, it is loaded into memory. Any changes made to that file must be explicitly saved with CTRL+S. 

## Brainstorming about configuration

- The DevOps operator can configure the database to auto save in specific intervals since it is assumed the operator will update the file a lot. If the file is inside a git repository then it can be configured to commit and push after each save.

- There are many configurations related to how devopsr behaves but all of that configuration is always tied to a given database. Devopsr for its own purpose **MUST NOT** have any configuration outside the database file.
  - For example, if someone wanted light mode/dark mode implementation, such functionality must be saved to the database itself (and NOT for example into a separate file, app data, Windows registry and such), even if it makes no sense. The "root of all DevOps" philosophy and single file with no other dependencies is too important. Devopsr itself must have NO other dependencies.

## Brainstorming about binaries

- Devopsr binary must be a single executable file, portable, no installation. It must not require other files to run.

## Brainstorming about tree and nodes

- Similar to keepass, everything important is organized into a tree. The tree has a single root and nodes nestable to any depth.

- The root has configurable properties related to the database as a whole (e.g. database metadata) and configuration affecting the devopsr tool itself.

- The node can represent anything required for DevOps operations: company, project, application, environment, deployment etc. The nomenclature is important and often completely different even between two projects in a single company. Sometimes projects define applications. Sometimes applications are used in projects.

- The node can also be used as a purely logical entity (like a folder/directory).

- Nodes have following **core properties**:
  - Name
  - Description
  - Icon shown in the tree (like in KeePass, I really like how that thing is created)

## Brainstorming about tabs
- Nodes are windows which have tabs, each tab has a type and a node can have only a single tab of a specific type.
- New node has no tab. To use a tab in the node you need to insert it there.
- Each tab type has a specific functionality and goals.
- There are following tab types:
  - Properties
  - Configuration
  - Scripts
  - Manual steps
  - Workflows
  - Documentation
 
- The root node has also the following areas to configure. These tabs are global configurations that can be present in the root node only.
  - Dependencies
  - Interpreters
  - Registry
  - Secret types
  - Logging
  - Expiration of script/workflow runs
  
## Brainstorming about tab types

- **Properties** tab contains a simple user managed key-value list of whatever semantically related to the node and its descendants. For example if the node is representing an "Application" then its property may be a "tech stack" with a value "ASP.NET website written in .NET 9". It is extension to the **core properties**.
  - The node can force all of its children to:
    - require a properties tab
    - inherit all keys or specific keys and forbid to change its value
    - allow all keys or specific keys to have a value
    - require all keys or specific keys to always have a non empty value
  - Property keys must be unique inside the node

- **Configuration** is also a key-value list but semantically related to the *configuration* of the node. For example if the node is "Application" then the configuration might be something like "DeploymentURL", "ServerName" etc.
  - A single configuration value can be marked as secret. You cannot do this with properties.
  - There is different semantics from properties:
    - Configuration cannot be inherited. It's always related to the node only.
    - Configuration keys have unique IDs automatically assigned when created. Therefore you can have multiple keys with same name.
    - Configuration can be **referenced** in another configuration (or anywhere else in Devopsr) either directly (by its ID) or by traversing the tree. Examples:
      - Tree traversal: `BACKEND_URL = https://myapp${{parent.configuration.ENV}}.azurewebsites.net` resolves to `https://myapptest.azurewebsites.net`
      - Direct ID reference: `DB_CONNECTION = Server=${{configid(12345)}};Database=myapp` resolves to `Server=sql.example.com;Database=myapp`
      - Multiple references: `API_URL = https://${{parent.configuration.HOST}}:${{parent.configuration.PORT}}/api` resolves to `https://api.example.com:8080/api`

## Brainstorming about configuration secrets

Configuration values can be marked as secrets. By default they are always masked with `***` if shown anywhere in the application and can be unmasked with a simple mouse click if required.

A configuration marked as a secret can also be marked as a *secret reference* but this requires a *secret type* defined in the node or any ancestor.

## Brainstorming about secret types
Secret types are managed by the user in the root node tab "Secret types".

A secret type has a name, description and a **read secret script** from the scripts tab of the node or any ancestor. Compulsory and optional parameters of the read script (defined in the script tab of the node or any ancestor) show as inputs when setting a configuration value as a secret reference.

## Brainstorming about scripts

A script has the following properties:

- **Interpreter**: devopsr must know the interpreter to run the script. When the script is run, devopsr simply spawns a new process and the interpreter is basically a template of how the process is be spawned. Interpreters can be configured by the user in the root node. Interpreters are things like bash, zsh, powershell, powershell core, php, py, npm, npmx, etc...
- **Input parameters** (0..*)
- **Script body** or **Script file path**.
- **Output parameters** --- interpreters don't have a notion of an output value, only standard I/O. The output parameters are captured from the stdout of the script by convention as JSON object surrounded by a configurable prefix and suffix, like `<devopser_output>` and `</devopser_output>`. For example:
  ```
  echo "doing work..."
  echo "<devopser_output>{"status": "success", "url": "https://example.com"}</devopser_output>"
  ```
  Devopsr considers only the last output generated this way and only after the script is finished.

Script body can be edited. Script can be run. Running script can be stopped. There should also be a button to open the file if the script is defined as having a file reference instead of the body.

When script is ran a "Script Run" object is created. A "Script Run" is the container for the started script which can store output into logs and can also produce logs (with a special formating of echo/Write-Host command) with various properties (trace, debug, info, warning, error....). The script tab stores only the "Script Runs" but the logs visible in the script run's details are stored in the context of the log.

## Brainstorming about manual steps

A manual step requires the node to have a documentation tab because all manual steps are automatically described in there. When a manual step is created, Devopsr automatically generates a new section in the documentation tab with a unique ID and formatted template. The DevOps operator then fills in the detailed instructions for this manual step. This ensures documentation stays in sync with the defined manual steps.

A manual step can describe multiple real life steps but that's up to the user how he chooses to describe the steps. The tab of manual steps is only the definition of these steps, they are meant to be used in the workflows.

A manual step can have input parameters and output parameters.
- **Input parameters** represent values given/created by previous steps of the workflow, they hint user to use these values for the manual steps.
- **Output parameters** represent values created by the user while doing the manual stuff. The values created in these steps might be required in the next script steps of the workflow.  

## Brainstorming about workflows

Workflow tab is a basic workflow editor that is intended to represent "DevOps operator's workflows" and not necessarily CI/CD pipelines. The semantics here are meant to represent IAC - things necessary to create/prepare/configure some kind of infrastructure.

Workflows contains ordered steps. Each step is either one of two types:
- manual step
- script

A manual step require the node to have a documentation tab because all manual steps are automatically described in there. The manual step represents something the DevOps operator needs to do manually before the next step and for the whole workflow to be completed.

A script is simply a reference to a script in the script tab of the node or any of its ancestors.

Workflow can be started. Starting a workflow creates a "Workflow run". Running workflow can be paused (although you need to wait for the script to finish) or stopped (this kills any running scripts).

When a workflow hits a manual step then user is asked to complete them manualy. When that is done, the user must confirm the step explicitly as completed for the workflow to continue.

## Brainstorming about logging

Everything the other tabs do is logged only if the root node configuration has logging enabled. In the future a logging tab attachable to the node could be implemented as well.

The user can configure to log everything into files or into the root node (in memory otherwise the database file would grow in size rapidly) but honestly the logging functionality is not what the DevOps operator needs from this tool. This tool is meant to be run manually and logs only serve an immediate debugging functionality.

## Brainstorming about documentation

Documentation is extremely important for DevOps operators. Any node can have any number of documentation tabs and these contain any user managed content. The first format that should be supported is markdown.

## Brainstorming about dependencies

Dependencies is a user managed list of anything that the database requires on a local system. E.g.

- terraform
- pscore
- az cli
- aws cli
- etc

A dependency contains a user provided name but also a user provided script that verifies whether the dependency is installed on the system or not.
