FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY /app/publish/aiof.api.core /app/
EXPOSE 80
ENTRYPOINT ["dotnet", "aiof.api.core.dll"]