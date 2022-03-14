FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backend/foodies-app.csproj", "backend/"]
RUN dotnet restore "backend/foodies-app.csproj"
COPY . .
WORKDIR "/src/backend"
RUN dotnet build "foodies-app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "foodies-app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "foodies-app.dll"]
