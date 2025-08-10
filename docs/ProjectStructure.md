# Project Structure

![ProjectStructure_Overview](/media/images/project-structure-overview.png)

A solid arrow represents a dependency (a project reference). The dashed arrows mean "use".

## Modules

Each bounded context is (usually) mapped to a module, similar to how a bounded context is mapped to a microservice. Most of the modules will follow an architectural style similar to that of clean architecture where the main focus is dependency inversion.

Each module will have a `Composition` project to basically provide extension methods for `IServiceCollection` and register module-specific services to the DI container in the startup project, which is the `Api` project.

Each module will also expose integration events (defined in the `IntegrationEvents` project) to other modules for inter-module communication:

![ProjectStructure_IntegrationEvents](/media/images/project-structure-integration-events.png)

This is similar to event-driven microservices, but, since modular monolith is still a monolith, everything is in a single process. This means we could use an in-memory or in-process event bus based on the pub/sub pattern to publish the integration event to other modules.

## Shared Kernel & Infrastructure

`Shared.Kernel` is where all the building blocks like abstract classes, interfaces, value objects, and things that are shared between the modules.

Since this is project is mainly for learning and researching I will also try to implement things myself like mediator and in-process event bus and the implementations are placed in the `Shared.Infrastructure` project. It's also referenced by the `Api` project to register the implementations in the DI container.
