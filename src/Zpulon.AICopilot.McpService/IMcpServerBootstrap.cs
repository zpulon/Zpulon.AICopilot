using ModelContextProtocol.Client;

namespace Zpulon.AICopilot.McpService;

public interface IMcpServerBootstrap
{
    IAsyncEnumerable<McpClient> StartAsync(CancellationToken cancellationToken);
}