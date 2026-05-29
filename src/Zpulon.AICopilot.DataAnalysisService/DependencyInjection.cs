using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zpulon.AICopilot.AgentPlugin;
using Zpulon.AICopilot.Dapper;
using Zpulon.AICopilot.DataAnalysisService.Services;
using Zpulon.AICopilot.Visualization;

namespace Zpulon.AICopilot.DataAnalysisService;

public static class DependencyInjection
{
    public static void AddDataAnalysisService(this IHostApplicationBuilder builder)
    {
        builder.AddDapper();
        builder.Services.AddScoped<VisualizationContext>();
        // 注册插件加载器
        builder.Services.AddAgentPlugin(registrar =>
        {
            registrar.RegisterPluginFromAssembly(Assembly.GetExecutingAssembly());
        });
    }
}