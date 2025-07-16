# DepresStore

> [!NOTE]
> This is not a real eCommerce system but an experimental project for learning and researching purposes where I try to apply DDD, CQRS and experiment with the modular monolith architecture. All business rules are made up.

## Bounded Contexts

**_Work In Progress_**

## Architecture Overview

**_Work In Progress_**

## Project Structure Overview (Modular Monolith)

Since this is a solo project both front-end and back-end are put in the same repo (monorepo). And instead of using folders like `frontend` and `backend`, I decided to use `client` for the client-side stuff (SPAs) and `server` for the server-side stuff (which includes both the back-end system and the customer-facing web app because it's server-side rendered).

Here's a general overview of the project structure:

![ProjectStructure_Overview](/media/images/project-structure-overview.png)

A solid arrow represents a dependency (a project reference). The dashed arrows are just for indicating that the web applications consume the REST API endpoints exposed by `WebApi`.

### Modules

Each bounded context is (usually) mapped to a module, similar to how a bounded context is usually mapped to a microservice. Most of the modules will follow an architectural style similar to that of clean architecture where the main focus is dependency inversion.

Each module will have a `Composition` project to basically provide extension methods for `IServiceCollection` and register module-specific services to the DI container in the startup project, which is `WebApi`.

Each module will also expose integration events (defined in the `IntegrationEvents` project) to other modules for inter-module communication:

![ProjectStructure_IntegrationEvents](/media/images/project-structure-integration-events.png)

This is similar to event-driven microservices, but, since modular monolith is still a monolith, everything is in a single process. This means we could use an in-memory or in-process event bus based on the pub/sub pattern to publish the integration event to other modules.

### Shared Kernel & Infrastructure

`Shared.Kernel` is where all the building blocks like abstract classes, interfaces, value objects, and things that are shared between the modules.

Since this is project is mainly for learning and researching I also tried to implement my own stuff like mediator and in-process event bus and the implementations are placed in the `Shared.Infrastructure` project. It's also referenced by `WebApi` to register the implementations in the DI container.

## CQRS

**_Work In Progress_**
