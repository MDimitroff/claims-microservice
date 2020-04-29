FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Claims.csproj", "./"]
RUN dotnet restore "./Claims.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Claims.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Claims.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Claims.dll"]