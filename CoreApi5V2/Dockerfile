#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CoreApi5V2/CoreApi5V2.csproj", "CoreApi5V2/"]
RUN dotnet restore "CoreApi5V2/CoreApi5V2.csproj"
COPY . .
WORKDIR "/src/CoreApi5V2"
RUN dotnet build "CoreApi5V2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreApi5V2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreApi5V2.dll"]