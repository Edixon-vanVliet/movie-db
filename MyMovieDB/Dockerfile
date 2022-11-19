FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5171

ENV ASPNETCORE_URLS=http://+:5171

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyMovieDB/MyMovieDB.csproj", "MyMovieDB/"]
RUN dotnet restore "MyMovieDB/MyMovieDB.csproj"
COPY . .
WORKDIR "/src/MyMovieDB"
RUN dotnet build "MyMovieDB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMovieDB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMovieDB.dll"]
