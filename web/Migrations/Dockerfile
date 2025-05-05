FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS tools
ARG version
RUN dotnet tool install dotnet-ef --global --version ${version}

FROM tools AS migrate
WORKDIR /src
COPY ["web/web.csproj", "web/"]
RUN dotnet restore "web/web.csproj"
COPY . .
WORKDIR "/src/web"
RUN dotnet tool restore

ENTRYPOINT ["Migrations/run.sh"]
