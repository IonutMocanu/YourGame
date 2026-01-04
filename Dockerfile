FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["APINet/APINet.csproj", "APINet/"]
COPY ["APINet.Shared/APINet.Shared.csproj", "APINet.Shared/"]
RUN dotnet restore "APINet/APINet.csproj"
COPY . .
WORKDIR "/src/APINet"
RUN dotnet publish "APINet.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "APINet.dll"]