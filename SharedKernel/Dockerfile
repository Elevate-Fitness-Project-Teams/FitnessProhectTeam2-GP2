FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files first for layer caching
COPY ["Elevate.Auth/Elevate.Auth.csproj", "Elevate.Auth/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
RUN dotnet restore "Elevate.Auth/Elevate.Auth.csproj"

# Copy everything else and build
COPY ["Elevate.Auth/", "Elevate.Auth/"]
COPY ["SharedKernel/", "SharedKernel/"]
RUN dotnet build "Elevate.Auth/Elevate.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elevate.Auth/Elevate.Auth.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elevate.Auth.dll"]