#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mover/Mover.csproj", "Mover/"]
COPY ["modules/Mover.Modules.FitDecoder/Mover.Modules.FitDecoder.csproj", "modules/Mover.Modules.FitDecoder/"]
COPY ["modules/Mover.Modules.FitDecoder.Shared/Mover.Modules.FitDecoder.Shared.csproj", "modules/Mover.Modules.FitDecoder.Shared/"]
COPY ["Mover.Shared/Mover.Shared.csproj", "Mover.Shared/"]
COPY ["modules/Mover.Modules.Uploader.Shared/Mover.Modules.Uploader.Shared.csproj", "modules/Mover.Modules.Uploader.Shared/"]
COPY ["modules/Mover.Modules.Uploader/Mover.Modules.Uploader.csproj", "modules/Mover.Modules.Uploader/"]
RUN dotnet restore "./Mover/Mover.csproj"
COPY . .
WORKDIR "/src/Mover"
RUN dotnet build "./Mover.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mover.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN chmod 777 /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mover.dll"]