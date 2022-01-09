# of course the .net repo names has changed bc reasons: https://github.com/dotnet/dotnet-docker/issues/2375


FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /hw1
COPY ["F1.Data/F1.Data.csproj", "F1.Data/"]
COPY ["F1.Web/F1Web.csproj", "F1.Web/"]
RUN dotnet restore "F1.Web/F1Web.csproj"
COPY . .
WORKDIR "/hw1/F1.Web"
RUN dotnet build "F1Web.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "F1Web.csproj" -c Release -o /app/publish
RUN dotnet ef database update

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1Web.dll"]
