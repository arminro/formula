


FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /hw1

# prepare migrations tool
# of course the .net repo names has changed bc reasons: https://github.com/dotnet/dotnet-docker/issues/2375
RUN dotnet tool install --global dotnet-ef --version 5.0.12
ENV PATH="${PATH}:/root/.dotnet/tools"


COPY ["F1.Data/F1.Data.csproj", "F1.Data/"]
COPY ["F1.Web/F1Web.csproj", "F1.Web/"]
# WORKDIR /hw1/FormulaServer
# COPY ["migrations-entrypoint.sh", "app/migrations-entrypoint.sh"]
# WORKDIR /hw1
RUN dotnet restore "F1.Web/F1Web.csproj"
COPY . .
WORKDIR "/hw1/F1.Web"
RUN dotnet build "F1Web.csproj" -c Release -o /app/build

# migration
# RUN dotnet ef database update
# CMD ["dotnet", "ef", "database", "update"]

FROM build as publish
RUN dotnet publish "F1Web.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1Web.dll"]

# running migrations
# RUN chmod +x migrations-entrypoint.sh
# CMD bin/bash ./migrations-entrypoint.sh

