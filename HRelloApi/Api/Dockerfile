FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HRelloApi/HRelloApi.csproj", "HRelloApi/"]
RUN dotnet restore "HRelloApi/HRelloApi.csproj"
COPY . .
WORKDIR "/src/HRelloApi"
RUN dotnet build "HRelloApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRelloApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRelloApi.dll"]
