# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["ResQue/ResQueApi.csproj", "ResQue/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["ResQueBackEnd/ResQueBackEnd.csproj", "ResQueBackEnd/"]
COPY ["ResQueDal/ResQueDal.csproj", "ResQueDal/"]

# Restore all projects
RUN dotnet restore "ResQue/ResQueApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ResQue"
RUN dotnet build "ResQueApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ResQueApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResQueApi.dll"]
