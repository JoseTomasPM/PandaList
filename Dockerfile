# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todo el proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore

# Publicar en Release
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar archivos publicados
COPY --from=build /app/out .

# Render asigna el puerto en la variable PORT
ENV ASPNETCORE_URLS=http://+:$PORT

# Nombre del DLL principal — cámbialo si tu proyecto tiene otro nombre
ENTRYPOINT ["dotnet", "PandaList.dll"]
