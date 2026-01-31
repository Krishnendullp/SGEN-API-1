# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution and all projects
COPY . .

# restore using solution
RUN dotnet restore SGen.sln

# publish API project
RUN dotnet publish services/BillingService/BillingService.Api/BillingService.Api.csproj \
    -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BillingService.Api.dll"]
