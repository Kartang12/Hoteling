#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 5000
#https://localhost:5000
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthorizationApi/AuthorizationApi.csproj", "AuthorizationApi/"]
COPY ["HotelingLibrary/HotelingLibrary.csproj", "HotelingLibrary/"]
RUN dotnet restore "AuthorizationApi/AuthorizationApi.csproj"
COPY . .
WORKDIR "/src/AuthorizationApi"
RUN dotnet build "AuthorizationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorizationApi.dll"]