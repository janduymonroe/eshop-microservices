#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Ordering/EShopMicroservice.Ordering.API/EShopMicroservice.Ordering.API.csproj", "src/Services/Ordering/EShopMicroservice.Ordering.API/"]
RUN dotnet restore "./src/Services/Ordering/EShopMicroservice.Ordering.API/EShopMicroservice.Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/EShopMicroservice.Ordering.API"
RUN dotnet build "./EShopMicroservice.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShopMicroservice.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopMicroservice.Ordering.API.dll"]