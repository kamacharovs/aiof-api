# Overview

All in one finance API

[![Build Status](https://gkamacharov.visualstudio.com/gkama-cicd/_apis/build/status/kamacharovs.aiof-api?branchName=master)](https://gkamacharov.visualstudio.com/gkama-cicd/_build/latest?definitionId=20&branchName=master)

## Documentation

aiof API overall documentation

### Application

On a high level, the API is a CRUD application for `aiof` data. It is based on a multi-tenant pattern based on `user_id`. This is enforced by the interface `ITenant` which gets dependency injected. Thus, even though User A is authenticated and authorized (via JWT), they can't access data for User B who is the same, authorization-wise, to user A. Thus the authentication and authorization process flow is as follows

- Is User Authenticated?
- Is User Authorized to access this Endpoint? Are they in the correct Role?
- Does access to the data the User is trying to get need to be controlled? If yes
- Then, `user_id` gets extracted from the JWT and EF Core uses `QueryFilters` to filter the data a User can access - in the current scenario, only their own

## How to run it

### Locally

From the root directory

```powershell
dotnet run -p .\aiof.api.core\
```

Change directories and run from the core `.csproj`

```powershell
cd .\aiof.api.core\
dotnet run
```

### Docker

Pull the latest image from Docker Hub

```powershell
docker pull gkama/aiof-api:latest
```

## EF Core migrations

Migrations are managed in the `ef-migrations` branch

```powershell
dotnet ef migrations add {migration name}
dotnet ef migrations script
dotnet ef migrations remove
```
