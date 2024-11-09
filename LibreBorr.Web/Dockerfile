FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LibreBorr.Web/LibreBorr.Web.csproj", "LibreBorr.Web/"]
COPY ["LibreBorr.BL/LibreBorr.BL.csproj", "LibreBorr.BL/"]
COPY ["LibreBorr.Services/LibreBorr.Services.csproj", "LibreBorr.Services/"]
RUN dotnet restore "LibreBorr.Web/LibreBorr.Web.csproj"
COPY . .
WORKDIR "/src/LibreBorr.Web"
RUN dotnet build "LibreBorr.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LibreBorr.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibreBorr.Web.dll"]
