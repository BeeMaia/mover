#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/Mover.Uploader/Mover.Uploader.csproj", "services/Mover.Uploader/"]
COPY ["shared/Mover.Shared/Mover.Shared.csproj", "shared/Mover.Shared/"]
COPY ["shared/Mover.Uploader.Shared/Mover.Uploader.Shared.csproj", "shared/Mover.Uploader.Shared/"]
RUN dotnet restore "./services/Mover.Uploader/Mover.Uploader.csproj"
COPY . .
WORKDIR "/src/services/Mover.Uploader"
RUN dotnet build "./Mover.Uploader.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mover.Uploader.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mover.Uploader.dll"]