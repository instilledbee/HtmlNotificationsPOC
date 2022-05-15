#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HtmlNotificationsPoc/HtmlNotificationsPoc.csproj", "HtmlNotificationsPoc/"]
RUN dotnet restore "HtmlNotificationsPoc/HtmlNotificationsPoc.csproj"
COPY . .
WORKDIR "/src/HtmlNotificationsPoc"
RUN dotnet build "HtmlNotificationsPoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlNotificationsPoc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HtmlNotificationsPoc.dll"]