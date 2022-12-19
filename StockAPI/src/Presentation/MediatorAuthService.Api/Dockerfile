FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80

COPY src/Domain/StockAPI.Domain.Core/*.csproj ./Domain/StockAPI.Domain.Core/
COPY src/Domain/StockAPI.Domain/*.csproj ./Domain/StockAPI.Domain/
COPY src/Infrastructure/StockAPI.Infrastructure/*.csproj ./Infrastructure/StockAPI.Infrastructure/
COPY src/Application/StockAPI.Application/*.csproj ./Application/StockAPI.Application/
COPY src/Presentation/StockAPI.Api/*.csproj ./Presentation/StockAPI.Api/

COPY ./*.sln .

RUN dotnet restore Presentation/StockAPI.Api/*.csproj
COPY . .
RUN dotnet publish src/Presentation/StockAPI.Api/*.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "StockAPI.Api.dll"]