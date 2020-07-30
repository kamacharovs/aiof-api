FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY /app/publish/aiof.api.core /app/
EXPOSE 80
ENTRYPOINT ["dotnet", "aiof.api.core.dll"]