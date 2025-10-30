# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todos los archivos del repo
COPY . .

# Restaurar dependencias
RUN dotnet restore

# Publicar SOLO el proyecto web, no la solución
RUN dotnet publish PandaList.csproj -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "PandaList.dll"]
