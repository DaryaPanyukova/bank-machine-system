FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Lab5/RenameMe/RenameMe.csproj", "src/Lab5/RenameMe/"]
RUN dotnet restore "src/Lab5/RenameMe/RenameMe.csproj"
COPY . .
WORKDIR "/src/src/Lab5/RenameMe"
RUN dotnet build "RenameMe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RenameMe.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RenameMe.dll"]
