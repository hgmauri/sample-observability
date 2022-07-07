FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Sample.Observability.WebApi/Sample.Observability.WebApi.csproj", "src/Sample.Observability.WebApi/"]
COPY ["src/Sample.Observability.Infrastructure/Sample.Observability.Infrastructure.csproj", "src/Sample.Observability.Infrastructure/"]
RUN dotnet restore "src/Sample.Observability.WebApi/Sample.Observability.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Sample.Observability.WebApi"
RUN dotnet build "Sample.Observability.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Observability.WebApi.csproj" -c Release -o /app/publish

ENV TZ=America/Sao_Paulo
ENV LANG pt-BR
ENV LANGUAGE pt-BR
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Observability.WebApi.dll"]
