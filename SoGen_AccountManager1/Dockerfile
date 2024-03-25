# Utiliser l'image SDK .NET pour la compilation
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SoGen_AccountManager1/SoGen_AccountManager1.csproj", "SoGen_AccountManager1/"]
RUN dotnet restore "SoGen_AccountManager1/SoGen_AccountManager1.csproj"
COPY . .
WORKDIR "/src/SoGen_AccountManager1"
RUN dotnet build "SoGen_AccountManager1.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "SoGen_AccountManager1.csproj" -c Release -o /app/publish

# Exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoGen_AccountManager1.dll"]
