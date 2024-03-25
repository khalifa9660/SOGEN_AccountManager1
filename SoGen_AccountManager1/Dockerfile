# Utiliser l'image SDK pour construire le code source dans l'image de build.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copier csproj et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier le reste des fichiers et construire
COPY . ./
RUN dotnet publish -c Release -o out

# Générer l'image de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SoGen_AccountManager1.dll"]
