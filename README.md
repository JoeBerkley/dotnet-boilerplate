

# ASP<span>.</span>NET Core Web Api Template
 
This project is an Web API Open-Source Boilerplate Template that includes ASP.NET Core 5, Web API standards, clean n-tier architecture, Redis, Mssql, with a lot of best practices.

## Prerequisites
- [Visual Studio 2019 Community and above](https://visualstudio.microsoft.com/)
- [.NET 5 SDK and above](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Suggested tools
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Technologies
- [ASP.NET Core 5 Web Api](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio)
- [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- [REST Standards](https://restfulapi.net/)
- [MSSQL](https://www.microsoft.com/en-gb/sql-server/sql-server-2019)
- [Microsoft Identity](https://docs.microsoft.com/en-us/azure/active-directory/develop/)
- [Redis](https://redis.io/)
- [SeriLog(seq)](https://serilog.net/)
- [AutoMapper](https://automapper.org/)
- [Smtp / Mailkit](https://github.com/jstedfast/MailKit)
- [Swagger Open Api](https://swagger.io/)
- [Health Checks](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0)
- [Docker](https://www.docker.com/)

## Features
- Net Core
- N-Tier Architecture
- Restful
- Entity Framework Core - Code First
- Repository Pattern - Generic
- Redis Caching
- Response Wrappers
- Microsoft Identity with JWT Authentication
- Role based Authorization
- Identity Seeding
- Database Seeding
- Custom Exception Handling Middlewares
- Serilog
- Automapper
- Swagger UI
- Healthchecks
- SMTP / Mailkit / Sendgrid Email Service
- Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
- Feature Flags

## Getting started

### Docker Compose
This comes with a `docker-compose.yml` file, to create an SQL express server, which will house both of our databases used in the project. To spin up this database, make sure you have Docker For Windows running, and then simply run `docker compose` from the root folder of the project.

### Running the API
To run the web app itself, it should be as simple as either, opening the solution in Visual Studio, ensuring the WebApi project is the default, and then start debugging.

If you instead wish to run it via a CLI, you can browse to the `/src` folder, and run the command `dotnet run --project ./Presentations/WebApi`. This will give you a local kestral server, with http on port 5000, and https on port 5001.

### Default Roles & Credentials
As soon you build and run your application, default users and roles get added to the database.

Default Roles:
- SuperAdmin
- Admin
- Moderator
- Basic

Here are the credentials for the default user.
- Email - dev@JoeBerkley.com 
- Password - D3fau1tPa55w0rd!

You can use these default credentials to generate valid JWTokens at the `/api/account/authenticate` endpoint.

## Project Structure

### Caching

The caching project is an implementation of a redis cache. This cache stores API responses, as well as database queries where applicable. An example on how to implement this on controller actions can be found in `WebApi/Controllers/AdminController`

### Core

Parts of code that make up the basis of the repository. Any services shared across multiple projects, extention methods, and any helpers or utilities that should be standard across the project.

### Data

Responsible for any interactions with the application database. This project contains an implementation of a generic repository pattern, to help abstract db calls away from the developer, helping keep the code DRY.

### Identity

Responsible for any interactions with the identity database, as well as generating JWT tokens for successful logins. This project also seeds its own database with default roles, and a default super admin account. If these roles are NOT found in the database, they will be recreated.

### Models

This defines the shape of all data in the system. Models stored in the `DbEntries` folder will automatically be modified in the database upon creation of a migration. 

### Services

This is a project for any services which don't fit into any of the above categories, and should be rarely used.

### Web App

The Web App is how any end user can interact with the programme. This is mostly a standard .NET Core API project. This project has Swagger (Wish Swashbuckle), Automapper, Health Checks, Feature Flags, all of which are registered and configured within `StartUp.cs`.

All mappings from DbEntities to items returned from an Api endpoint are registered within the `Helpers/MappingProfiles.cs` file. If your class is getting large, it may be worth breaking this out into its own folder, and registering them all using reflection, simply for organization.

#### Feature Management

Feature Management is enabled on this project, however when adding a new feature, there is a small amount of leg work to do. Firstly, in `Core/Enums/Features.cs` you will need to add the name of the feature. Next, add the feature and whether it is enabled to the `appsettings.json` file, within the `FeatureManagement` block. From there, you can use the feature block on actions, entire controllers, or even blocks of code. For a full example on how to use Feature Management please see the [README](https://github.com/microsoft/FeatureManagement-Dotnet/blob/main/README.md) from Microsoft.

Because this project uses Feature Management, something which Swagger/Swashbuckle doesn't support out of the box, We've had to make a custom filter made to only handle routes that are actually enabled to the end user.

The project has its own custom attributes for caching, so that the `Cache` project knows how to handle the cache within Redis.

#### Swagger
You can view endpoints with swagger
![Basic Swagger end points](https://i.imgur.com/pqsNyeV.png)

#### HealthCheck
You can check the status of the services with HealthCheck. This can be viewed at any time by browsing to `/health`. By default, HealthCheck will check the status of Redis, the ApplicationDB and the IdentityDb. This is returned as a JSON object, if you visit the end point, however, we also have direct access to the health of these objects within the code.