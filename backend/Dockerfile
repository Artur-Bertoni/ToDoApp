FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "CSharpBackend.csproj"

RUN dotnet tool install --global dotnet-ef --version 6.0.0

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet build "CSharpBackend.csproj" -c Release -o /app/build

RUN dotnet publish "CSharpBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
