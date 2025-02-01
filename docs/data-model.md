# Devopsr Data Model

## Core Entities

### Database
- Single file container (.devopsr extension)
- Properties:
  - Auto-save configuration
  - Git integration settings
  - UI preferences (e.g. dark/light mode)

### Node
- Core Properties:
  - Name
  - Description 
  - Icon
- Can contain multiple tabs
- Hierarchical structure (tree)
- Can represent:
  - Company
  - Project
  - Application
  - Environment
  - Deployment
  - Logical grouping

### Tab
- Types:
  - Properties
  - Configuration
  - Scripts
  - Manual Steps
  - Workflows
  - Documentation
- One tab type per node maximum

### Property (in Properties Tab)
- Key-value pairs
- Unique keys within node
- Inheritance rules for child nodes:
  - Required properties
  - Inherited values
  - Value constraints

### Configuration Entry
- Properties:
  - Key
  - Value
  - Unique ID
  - Secret flag
  - Secret reference flag (optional)
- Can reference other configurations
- No inheritance

### Script
Properties:
- Interpreter reference
- Input parameters (0..*)
- Script content or file path
- Output parameters
- Execution state

### Script Run
Properties:
- Reference to Script
- Execution logs
- Output values
- Status

### Manual Step
Properties:
- Input parameters
- Output parameters
- Documentation reference
- Status

### Workflow
Properties:
- Ordered steps (Script or Manual Step)
- Execution state

### Workflow Run
Properties:
- Reference to Workflow
- Current step
- Status
- Step completion states

### Secret Type
Properties:
- Name
- Description
- Read secret script reference
- Parameters

### Interpreter
Properties:
- Name
- Process spawn template
- Parameters

## Root-Only Entities

### Dependencies
- Name
- Verification script
- Status

### Registry
- System-wide settings

### Logging Configuration
- Enable/disable
- Storage location
- Log level
- Retention policy

## Relationships

- Database contains exactly one Root Node
- Node can have 0..* child Nodes
- Node can have 0..* Tabs (max 1 per type)
- Properties Tab contains 0..* Property entries
- Configuration Tab contains 0..* Configuration entries
- Scripts Tab contains 0..* Scripts
- Manual Steps Tab contains 0..* Manual Steps
- Workflow Tab contains 0..* Workflows
- Documentation Tab contains formatted content
- Script can reference 0..1 Interpreter
- Manual Step must reference 1 Documentation section
- Workflow contains 1..* ordered Steps
- Secret Type references 1 Script
