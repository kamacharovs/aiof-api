# Overview

All in one finance API

[![Build Status](https://gkamacharov.visualstudio.com/gkama-cicd/_apis/build/status/kamacharovs.aiof-api?branchName=master)](https://gkamacharov.visualstudio.com/gkama-cicd/_build/latest?definitionId=20&branchName=master)

## Documentation

aiof API overall documentation

### Application

On a high level, the API is a CRUD application for `aiof` data. It is based on a multi-tenant pattern with `user_id`. This is enforced by the interface `ITenant` which gets dependency injected. Even though User A is authenticated and authorized (via JWT), they can't access data for User B who is the same, authorization-wise, to user A. Thus, the authentication and authorization process flow is as follows:

- Is User Authenticated?
- Is User Authorized to access this Endpoint? Are they in the correct Role?
- Does access to the data the User is trying to get need to be controlled? If yes
- Then, `user_id` gets extracted from the JWT claim and EF Core uses `QueryFilters` to filter the data a User can access - in the current scenario, only their own

## How to run it

From the root project directory

```powershell
dotnet run -p .\aiof.api.core\
```

Or change directories and run from the core `.csproj`

```powershell
cd .\aiof.api.core\
dotnet run
```

Note: the API uses JWT authentication and authorization. In order to run this locally with fake in-memory data and bypass the authorization and authentication, you can play around with the code and comment out the `[Authorize]` attributes in the `Controllers`.

If you want to fully test it, then the recommended way is to use `docker-compose`. That pulls down all the Docker images needed and you will have the full microservices architecture locally in order to get a JWT from `aiof-auth` and add it to your requests to this API.

### Docker

Pull the latest image from Docker Hub

```powershell
docker pull gkama/aiof-api:latest
```

Run it

```powershell
docker run -it --rm -e ASPNETCORE_ENVIRONMENT='Development' -p 8000:80 gkama/aiof-api:latest
```

Make API calls to

```text
http://localhost:8000/
```

### Docker compose

From the project root directory

```powershell
docker-compose up
```

Make API calls to

```text
http://localhost:8000   aiof-api
http://localhost:8001   aiof-auth
http://localhost:8002   aiof-metadata
```

## EF Core migrations

Migrations are managed in the `ef-migrations` branch

```powershell
dotnet ef migrations add {migration name}
dotnet ef migrations script
dotnet ef migrations script 20180904195021_InitialCreate
dotnet ef migrations remove
```
