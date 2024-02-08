FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MiniHbys.Web/MiniHbys.Web.csproj", "MiniHbys.Web/"]
RUN dotnet restore "MiniHbys.Web/MiniHbys.Web.csproj"
COPY . .
WORKDIR "/src/MiniHbys.Web"
RUN dotnet build "MiniHbys.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniHbys.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniHbys.Web.dll"]
