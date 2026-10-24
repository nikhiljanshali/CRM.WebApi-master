# GlowStar CRM

GlowStar CRM is a modular ASP.NET Core 3.1 API solution designed for customer relationship management, supplier and seller workflows, pricing operations, identity management, and file processing. The solution is organized as a collection of web APIs that share common infrastructure, business logic, and data access projects, making it easy to extend and deploy as a multi-service backend.

## Overview

This repository contains the backend for a CRM platform serving several business domains, including:

- customer registration and management
- seller and supplier organization data
- employee and branch information
- pricing requests and price history
- file upload and retrieval services
- authentication and authorization with JWT
- workflow and notification support
- centralized logging and reusable infrastructure

The project is structured around multiple ASP.NET Core services that connect to SQL Server and MongoDB, with Swagger enabled for API exploration and testing.

## Solution structure

The solution includes the following projects:

- CRM.Admin - administrative API and system user management
- CRM.Customer - customer-facing APIs and registration services
- CRM.Supplier - supplier-related APIs
- CRM.Seller - seller and organization APIs
- CRM.Pricing - pricing request and pricing history APIs
- CRM.FileApi - file upload, download, and media conversion endpoints
- CRM.IdentityServer - authentication, JWT issuance, and refresh-token support
- CRM.Notification - notification service
- CRM.Workflow - workflow-related service
- CRM.Core - shared infrastructure, base configuration, and core services
- CRM.DAL - data access repositories and persistence logic
- CRM.Business - business layer services and logic
- CRM.Entities - shared domain entities
- CRM.Logging - logging and monitoring abstractions

## Technology stack

- ASP.NET Core 3.1
- C# / .NET
- Swagger / Swashbuckle
- JWT bearer authentication
- ASP.NET Core Identity with MongoDB support
- SQL Server for relational data stores
- MongoDB for document-based storage, logging, and authentication data
- Newtonsoft.Json for API serialization

## Key platform behavior

From the project configuration and controllers, the system follows a layered API design:

- HTTP controllers expose domain-specific endpoints such as registration, organization access, employee lookups, pricing requests, and file operations
- repositories in the DAL layer provide MongoDB and SQL-backed access patterns
- business and infrastructure projects centralize shared logic, validation, and logging
- JWT-based security is configured across services, with Swagger integration for bearer-token testing

Examples of API areas in the codebase include:

- `api/Register` for customer registration workflows
- `api/Organization` and `api/Employee` for organization management
- `api/PriceRequest` for pricing operations
- `api/fileupload` for upload and retrieval of files
- identity endpoints for auth and token flows

## Prerequisites

Before running the project locally, make sure you have:

- .NET Core SDK 3.1
- Visual Studio 2019 or later, or another .NET-compatible IDE
- SQL Server instance (the solution configuration uses SQL Server connection strings)
- MongoDB instance
- access to the configured internal database hosts and credentials used in the appsettings files

## Configuration

Each API project contains an `appsettings.json` file with connection strings and authentication settings. Update the values in the relevant project before running the service locally.

Important settings include:

- `Data:DefaultConnection:SqlConnectionString`
- `Data:DefaultConnection:MongoConnectionString`
- `DbLog:ConnectionString`
- `TokenAuthentication:Issuer`
- `TokenAuthentication:Audience`
- `TokenAuthentication:client_secret`

The current configuration references a local environment with SQL Server and MongoDB endpoints such as `172.20.15.58`, so those values should be adjusted for your deployment environment.

## Getting started

1. Clone the repository.
2. Open the solution file `CRM.WebApi.sln` in Visual Studio or your IDE.
3. Restore NuGet packages:

   ```bash
   dotnet restore
   ```

4. Build the solution:

   ```bash
   dotnet build CRM.WebApi.sln
   ```

5. Update connection strings and authentication settings in the relevant `appsettings.json` files.
6. Run the desired service project, for example:

   ```bash
   dotnet run --project CRM.Admin/CRM.Admin.csproj
   ```

   or

   ```bash
   dotnet run --project CRM.IdentityServer/CRM.IdentityServer.csproj
   ```

7. Open the Swagger UI for the running API at the usual endpoint, typically:

   ```text
   https://localhost:<port>/swagger
   ```

## Build and test

The repository is primarily an API solution rather than a test-heavy application. The typical validation flow is:

```bash
dotnet restore
dotnet build CRM.WebApi.sln
```

For runtime verification, start one or more projects and test endpoints through Swagger or an HTTP client.

## API documentation

Swagger is configured in the ASP.NET Core startup classes for each API project. This allows endpoints to be discovered and tested directly in the browser via the Swagger UI.

## Contribution guidelines

Contributions are welcome. To contribute:

1. Create a feature branch from the main codebase.
2. Keep changes focused and aligned with the existing layered architecture.
3. Update config and documentation when introducing new environment-specific settings.
4. Validate builds locally before opening a pull request.
5. Submit a clear description of the problem, solution, and affected API areas.

## Notes

This repository contains multiple independently runnable backend services, not a single monolithic application. When working on a feature, identify the relevant service project first and keep configuration, route patterns, and persistence settings consistent with that project.

## License

This project does not include an explicit license file in the repository snapshot, so usage and redistribution should be confirmed with the repository owner before production deployment.
