using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zpulon.AICopilot.Embedding;
using Zpulon.AICopilot.EventBus;

namespace Zpulon.AICopilot.RagService;

public static class DependencyInjection
{
    public static void AddRagService(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        builder.AddEventBus();
        builder.AddEmbedding();
    }
}