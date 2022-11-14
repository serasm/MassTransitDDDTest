FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 84
EXPOSE 445

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Domain.Shared/Domain.Shared.csproj", "Domain.Shared/"]
COPY ["Users.Domain/Users.Domain.csproj", "Users.Domain/"]
COPY ["Users.Database/Users.Database.csproj", "Users.Database/"]
COPY ["Users.Migrations/Users.Migrations.csproj", "Users.Migrations/"]
RUN dotnet restore "Users.Migrations/Users.Migrations.csproj"
COPY . .
WORKDIR "/src/Users.Migrations"
RUN dotnet build "Users.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Migrations.dll"]
