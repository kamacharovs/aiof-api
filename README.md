# Overview

All in one finance API

[![Build Status](https://gkamacharov.visualstudio.com/gkama-cicd/_apis/build/status/kamacharovs.aiof-api?branchName=master)](https://gkamacharov.visualstudio.com/gkama-cicd/_build/latest?definitionId=20&branchName=master)

## Documentation

aiof API overall documentation

### EF Core

Need to address User A accessing data for user B when authenticated. Thus, this will be a multi-tenant application

## Docker

```ps
docker pull gkama/aiof-api:latest
```

### EF Core migrations

Migrations are managed in the `ef-migrations` branch

```powershell
dotnet ef migrations add {migration name}
dotnet ef migrations script
dotnet ef migrations remove
```
