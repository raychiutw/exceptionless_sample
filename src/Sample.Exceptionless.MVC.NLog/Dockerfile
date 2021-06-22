#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Sample.Exceptionless.MVC.NLog/Sample.Exceptionless.MVC.NLog.csproj", "Sample.Exceptionless.MVC.NLog/"]
RUN dotnet restore "Sample.Exceptionless.MVC.NLog/Sample.Exceptionless.MVC.NLog.csproj"
COPY . .
WORKDIR "/src/Sample.Exceptionless.MVC.NLog"
RUN dotnet build "Sample.Exceptionless.MVC.NLog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Exceptionless.MVC.NLog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Exceptionless.MVC.NLog.dll"]