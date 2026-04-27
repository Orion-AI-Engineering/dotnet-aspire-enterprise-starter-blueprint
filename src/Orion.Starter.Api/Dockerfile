FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN echo "Placeholder Dockerfile for packaging guidance"
FROM base AS final
COPY --from=build /src /app
ENTRYPOINT ["dotnet", "Orion.Starter.Api.dll"]
