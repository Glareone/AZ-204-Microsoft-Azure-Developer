#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LocalFunctionProject/LocalFunctionProject.csproj", "LocalFunctionProject/"]
RUN dotnet restore "LocalFunctionProject/LocalFunctionProject.csproj"
COPY . .
WORKDIR "/src/LocalFunctionProject"
RUN dotnet build "LocalFunctionProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocalFunctionProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true