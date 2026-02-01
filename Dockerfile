# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore SGen.sln

RUN dotnet publish services/BillingService/BillingService.Api/BillingService.Api.csproj \
    -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

# 🔥 IMPORTANT: match Program.cs
ENV ASPNETCORE_URLS=http://+:7072
EXPOSE 7072

ENTRYPOINT ["dotnet", "BillingService.Api.dll"]

