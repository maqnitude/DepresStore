# DepresStore

> [!NOTE]
> This project aims to simulate an e-commerce system to explore DDD, CQRS, modular monolith architecture, and different frameworks/libraries. It's not intended as a real system but serves as a learning tool inspired by real e-commerce sites and personal preferences.

Details on the requirements can be found [here](/docs/Requirements.md).

## Architecture Overview

> [!WARNING]
> Work in progress.

Here's an overview of the modular monolith architecture:

![Architecture Overview](/media/images/Architecture_Overview.png)

More details can be found in [here](/docs/Architecture.md).

## Project Structure Overview

Here's a general overview of the project structure:

![ProjectStructure_Overview](/media/images/project-structure-overview.png)

More details can be found in [here](/docs/ProjectStructure.md).

## Features

> [!WARNING]
> Work in progress.

- Authentication and authorization using OAuth 2.0/OpenID Connect via OpenIddict in combination with ASP.NET Core Identity

## Technologies

- **Front-end**: React, Vite, MaterialUI, ASP.NET Core (MVC, Razor Pages), Bootstrap
- **Back-end**: ASP.NET Core (minimal API, Identity), Entity Framework Core, OpenIddict
- **Database**: SQL Server

## Local Setup

The projects will be running on these ports:

- `backoffice`: `https://localhost:3000`
- `DepresStore.Storefront`: `http://localhost:5002` and `https://localhost:7002`
- `DepresStore.AuthorizationServer`: `http://localhost:5001` and `https://localhost:7001`
- `DepresStore.Api`: `http://localhost:5000` and `https://localhost:7000`

Use [`mkcert`](https://github.com/FiloSottile/mkcert) to create the local development certificates for Vite. `cd` into `frontend/backoffice` and create the key and cert files:

```bash
mkdir cert

mkcert -key-file cert/localhost-key.pem -cert-file cert/localhost-cert.pem localhost
```

Use `libman restore` (see [Microsoft.Web.LibraryManager.Cli](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli)) to restore the packages for the `DepresStore.Storefront` and `DepresStore.AuthorizationServer` projects.

Run the projects with HTTPS profile for OpenIddict to work:

```bash
dotnet run --launch-profile https
```

## License

This project is licensed under the MIT License.
