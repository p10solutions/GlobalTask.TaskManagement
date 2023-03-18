#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GlobalTask.TaskManagement.Worker/GlobalTask.TaskManagement.Worker.csproj", "GlobalTask.TaskManagement.Worker/"]
RUN dotnet restore "GlobalTask.TaskManagement.Worker/GlobalTask.TaskManagement.Worker.csproj"
COPY . .
WORKDIR "/src/GlobalTask.TaskManagement.Worker"
RUN dotnet build "GlobalTask.TaskManagement.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GlobalTask.TaskManagement.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlobalTask.TaskManagement.Worker.dll"]