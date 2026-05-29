using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zpulon.AICopilot.EntityFrameworkCore;
using Zpulon.AICopilot.IdentityService.Contracts;
using Zpulon.AICopilot.Infrastructure.Authentication;
using Zpulon.AICopilot.Infrastructure.Storage;
using Zpulon.AICopilot.Services.Common.Contracts;

namespace Zpulon.AICopilot.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructures(this IHostApplicationBuilder builder)
    {
        builder.AddEfCore();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        builder.Services.AddSingleton<IFileStorageService, LocalFileStorageService>();
    }   
}