FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnboardingSIGDB1.API/OnboardingSIGDB1.API.csproj", "OnboardingSIGDB1.API/"]
RUN dotnet restore "OnboardingSIGDB1.API/OnboardingSIGDB1.API.csproj"
COPY . .
WORKDIR "/src/OnboardingSIGDB1.API"
RUN dotnet build "OnboardingSIGDB1.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OnboardingSIGDB1.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnboardingSIGDB1.API.dll"]
