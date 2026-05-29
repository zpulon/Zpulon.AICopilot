using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zpulon.AICopilot.AgentPlugin;
using Zpulon.AICopilot.AiGatewayService.Agents;
using Zpulon.AICopilot.AiGatewayService.Workflows;

namespace Zpulon.AICopilot.AiGatewayService;

public static class DependencyInjection
{
    public static void AddAiGatewayService(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        builder.Services.AddScoped<ChatAgentFactory>();
        
        builder.Services.AddHttpClient("OpenAI", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddAgentPlugin(registrar =>
        {
            registrar.RegisterPluginFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        builder.Services.AddScoped<IntentRoutingAgentBuilder>();
        
        builder.Services.AddScoped<DataAnalysisAgentBuilder>();
        
        builder.AddIntentWorkflow();
    }
}