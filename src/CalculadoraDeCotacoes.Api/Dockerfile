FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CalculadoraDeCotacoes.Api/CalculadoraDeCotacoes.Api.csproj", "src/CalculadoraDeCotacoes.Api/"]
COPY ["src/CalculadoraDeCotacoes.Application/CalculadoraDeCotacoes.Application.csproj", "src/CalculadoraDeCotacoes.Application/"]
COPY ["src/CalculadoraDeCotacoes.Domain/CalculadoraDeCotacoes.Domain.csproj", "src/CalculadoraDeCotacoes.Domain/"]
COPY ["src/CalculadoraDeCotacoes.Persistence/CalculadoraDeCotacoes.Persistence.csproj", "src/CalculadoraDeCotacoes.Persistence/"]
RUN dotnet restore "src/CalculadoraDeCotacoes.Api/CalculadoraDeCotacoes.Api.csproj"
COPY . .
WORKDIR "/src/src/CalculadoraDeCotacoes.Api"
RUN dotnet build "CalculadoraDeCotacoes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CalculadoraDeCotacoes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculadoraDeCotacoes.Api.dll"]
