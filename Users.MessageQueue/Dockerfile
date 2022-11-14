FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Domain.Shared/Domain.Shared.csproj", "Domain.Shared/"]
COPY ["Users.Domain/Users.Domain.csproj", "Users.Domain/"]
COPY ["Users.Database/Users.Database.csproj", "Users.Database/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
COPY ["Users.Infrastructure/Users.Infrastructure.csproj", "Users.Infrastructure/"]
COPY ["Users.MessageQueue/Users.MessageQueue.csproj", "Users.MessageQueue/"]
RUN dotnet restore "Users.MessageQueue/Users.MessageQueue.csproj"
COPY . .
WORKDIR "/src/Users.MessageQueue"
RUN dotnet build "Users.MessageQueue.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.MessageQueue.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.MessageQueue.dll"]
