FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["WMMPTextAnalyzer/WMMPTextAnalyzer.csproj", "WMMPTextAnalyzer/"]

RUN dotnet restore "WMMPTextAnalyzer/WMMPTextAnalyzer.csproj"
COPY . .
WORKDIR "/src/WMMPTextAnalyzer"
RUN dotnet build "WMMPTextAnalyzer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WMMPTextAnalyzer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WMMPTextAnalyzer.dll"]