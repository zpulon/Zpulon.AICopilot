FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Zpulon.AICopilot.AppHost/Zpulon.AICopilot.AppHost.csproj", "src/Zpulon.AICopilot.AppHost/"]
COPY ["src/Zpulon.AICopilot.HttpApi/Zpulon.AICopilot.HttpApi.csproj", "src/Zpulon.AICopilot.HttpApi/"]
COPY ["src/Zpulon.AICopilot.SharedKernel/Zpulon.AICopilot.SharedKernel.csproj", "src/Zpulon.AICopilot.SharedKernel/"]
COPY ["src/Zpulon.AICopilot.Services.Common/Zpulon.AICopilot.Services.Common.csproj", "src/Zpulon.AICopilot.Services.Common/"]
COPY ["src/Zpulon.AICopilot.Core.AiGateway/Zpulon.AICopilot.Core.AiGateway.csproj", "src/Zpulon.AICopilot.Core.AiGateway/"]
COPY ["src/Zpulon.AICopilot.Core.Rag/Zpulon.AICopilot.Core.Rag.csproj", "src/Zpulon.AICopilot.Core.Rag/"]
COPY ["src/Zpulon.AICopilot.Core.DataAnalysis/Zpulon.AICopilot.Core.DataAnalysis.csproj", "src/Zpulon.AICopilot.Core.DataAnalysis/"]
COPY ["src/Zpulon.AICopilot.Core.McpServer/Zpulon.AICopilot.Core.McpServer.csproj", "src/Zpulon.AICopilot.Core.McpServer/"]
COPY ["src/Zpulon.AICopilot.EntityFrameworkCore/Zpulon.AICopilot.EntityFrameworkCore.csproj", "src/Zpulon.AICopilot.EntityFrameworkCore/"]
COPY ["src/Zpulon.AICopilot.IdentityService/Zpulon.AICopilot.IdentityService.csproj", "src/Zpulon.AICopilot.IdentityService/"]
COPY ["src/Zpulon.AICopilot.ServiceDefaults/Zpulon.AICopilot.ServiceDefaults.csproj", "src/Zpulon.AICopilot.ServiceDefaults/"]
COPY ["src/Zpulon.AICopilot.AiGatewayService/Zpulon.AICopilot.AiGatewayService.csproj", "src/Zpulon.AICopilot.AiGatewayService/"]
COPY ["src/Zpulon.AICopilot.AgentPlugin/Zpulon.AICopilot.AgentPlugin.csproj", "src/Zpulon.AICopilot.AgentPlugin/"]
COPY ["src/Zpulon.AICopilot.EventBus/Zpulon.AICopilot.EventBus.csproj", "src/Zpulon.AICopilot.EventBus/"]
COPY ["src/Zpulon.AICopilot.Embedding/Zpulon.AICopilot.Embedding.csproj", "src/Zpulon.AICopilot.Embedding/"]
COPY ["src/Zpulon.AICopilot.Dapper/Zpulon.AICopilot.Dapper.csproj", "src/Zpulon.AICopilot.Dapper/"]
COPY ["src/Zpulon.AICopilot.RagService/Zpulon.AICopilot.RagService.csproj", "src/Zpulon.AICopilot.RagService/"]
COPY ["src/Zpulon.AICopilot.DataAnalysisService/Zpulon.AICopilot.DataAnalysisService.csproj", "src/Zpulon.AICopilot.DataAnalysisService/"]
COPY ["src/Zpulon.AICopilot.Visualization/Zpulon.AICopilot.Visualization.csproj", "src/Zpulon.AICopilot.Visualization/"]
COPY ["src/Zpulon.AICopilot.Infrastructure/Zpulon.AICopilot.Infrastructure.csproj", "src/Zpulon.AICopilot.Infrastructure/"]
COPY ["src/Zpulon.AICopilot.McpService/Zpulon.AICopilot.McpService.csproj", "src/Zpulon.AICopilot.McpService/"]
COPY ["src/Zpulon.AICopilot.MigrationWorkApp/Zpulon.AICopilot.MigrationWorkApp.csproj", "src/Zpulon.AICopilot.MigrationWorkApp/"]
COPY ["src/Zpulon.AICopilot.RagWorker/Zpulon.AICopilot.RagWorker.csproj", "src/Zpulon.AICopilot.RagWorker/"]
RUN dotnet restore "src/Zpulon.AICopilot.AppHost/Zpulon.AICopilot.AppHost.csproj"
COPY . .
WORKDIR "/src/src/Zpulon.AICopilot.AppHost"
RUN dotnet build "./Zpulon.AICopilot.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Zpulon.AICopilot.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zpulon.AICopilot.AppHost.dll"]
