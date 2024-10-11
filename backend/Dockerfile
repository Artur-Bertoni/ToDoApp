## Etapa 1: Build
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /app
#
## Copia os arquivos e restaura dependências
#COPY . ./
#RUN dotnet restore
#
## Build do projeto
#RUN dotnet build -c Release -o /app/build
#
## Publicação do projeto
#RUN dotnet publish -c Release -o /app/publish
#
## Etapa 2: Runtime
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
#WORKDIR /app
#COPY --from=build /app/publish .
#
## Exposição da porta do backend
#EXPOSE 5000
#
## Início do aplicativo
#ENTRYPOINT ["dotnet", "CSharpBackend.dll"]




# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia os arquivos e restaura dependências
COPY . ./
RUN dotnet restore

# Build do projeto
RUN dotnet build -c Release -o /app/build

# Publicação do projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exposição da porta do backend
EXPOSE 5000

# Comando para aplicar as migrações e rodar o aplicativo
ENTRYPOINT ["bash", "-c", "dotnet ef database update && dotnet CSharpBackend.dll"]
