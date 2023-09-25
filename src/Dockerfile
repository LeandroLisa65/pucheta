FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
# FROM bitnami/dotnet-sdk:6.0.414 AS build
ENV ASPNETCORE_URLS=http://+:80

WORKDIR /app

# Copiar y restaurar proyectos de EF
COPY Gestion.EF/Gestion.EF.csproj Gestion.EF/
RUN dotnet restore Gestion.EF/Gestion.EF.csproj
COPY Gestion.EF/ Gestion.EF/

# Copiar y restaurar proyectos de Negocio
COPY Gestion.Negocio/Gestion.Negocio.csproj Gestion.Negocio/
RUN dotnet restore Gestion.Negocio/Gestion.Negocio.csproj
COPY Gestion.Negocio/ Gestion.Negocio/

# Construir y publicar el proyecto de Web
FROM build AS publish
WORKDIR /app/Gestion.Web
COPY Gestion.Web/*.csproj .
RUN dotnet restore
COPY Gestion.Web/ . 
RUN dotnet publish -c Release -o /app/Gestion.Web/publish /p:PublishWithAspNetCoreTargetManifest="false"

# Construir la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
# FROM bitnami/aspnet-core:6.0.9
ENV TZ="America/Argentina/Cordoba"
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app/Gestion.Web
COPY --from=publish /app/Gestion.Web/publish .
ENTRYPOINT ["dotnet", "Gestion.Web.dll"]