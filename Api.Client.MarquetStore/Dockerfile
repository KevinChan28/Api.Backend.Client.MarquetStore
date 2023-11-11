#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Api.Client.MarquetStore/Api.Client.MarquetStore.csproj", "Api.Client.MarquetStore/"]
RUN dotnet restore "Api.Client.MarquetStore/Api.Client.MarquetStore.csproj"
COPY . .
WORKDIR "/src/Api.Client.MarquetStore"
RUN dotnet build "Api.Client.MarquetStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Client.MarquetStore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configura las variables de entorno
ENV EMAIL_PATH=Views/

ENTRYPOINT ["dotnet", "Api.Client.MarquetStore.dll"]