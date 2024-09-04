# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Tasks-BE/Tasks-BE.csproj", "Tasks-BE/"]
COPY ["Tasks.BLL/Tasks.BLL.csproj", "Tasks.BLL/"]
COPY ["Tasks.Common/Tasks.Common.csproj", "Tasks.Common/"]
COPY ["Tasks.Entities/Tasks.Entities.csproj", "Tasks.Entities/"]
COPY ["Tasks.DAL/Tasks.DAL.csproj", "Tasks.DAL/"]
RUN dotnet restore "./Tasks-BE/Tasks-BE.csproj"
COPY . .
WORKDIR "/src/Tasks-BE"
RUN dotnet build "./Tasks-BE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Tasks-BE.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks-BE.dll"]