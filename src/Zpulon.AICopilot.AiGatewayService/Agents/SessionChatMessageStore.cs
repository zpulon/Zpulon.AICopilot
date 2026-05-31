using MediatR;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zpulon.AICopilot.AiGatewayService.Queries.Sessions;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.Sessions;
using Zpulon.AICopilot.SharedKernel.Repository;

namespace Zpulon.AICopilot.AiGatewayService.Agents;

public record SessionSoreState(Guid SessionId, int MessageCount = 20);

public class SessionChatMessageStore : ChatHistoryProvider
{
    private readonly SessionSoreState? _sessionSoreState;
    
    private readonly IServiceProvider _serviceProvider;

    public SessionChatMessageStore(IServiceProvider serviceProvider, SessionSoreState? state = null)
    {
        _serviceProvider = serviceProvider;
        _sessionSoreState = state;
    }


    /// <summary>
    /// 替代原有的 GetMessagesAsync
    /// </summary>
    protected override async ValueTask<IEnumerable<ChatMessage>> ProvideChatHistoryAsync(
        InvokingContext context,
        CancellationToken cancellationToken = default)
    {
        if (_sessionSoreState == null) return [];

        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var query = new GetListChatMessagesQuery(_sessionSoreState.SessionId, _sessionSoreState.MessageCount);
        var result = await mediator.Send(query, cancellationToken);

        return result.Value?? [];
    }

    /// <summary>
    /// 替代原有的 AddMessagesAsync
    /// </summary>
    protected override async ValueTask StoreChatHistoryAsync(
        InvokedContext context,
        CancellationToken cancellationToken = default)
    {
        if (_sessionSoreState == null) return;

        // context.RequestMessages 经过默认 Filter 过滤，只包含本轮新增的用户消息
        // context.ResponseMessages 包含本轮 Agent 生成的响应消息
        var newMessages = context.RequestMessages.Concat(context.ResponseMessages ?? Array.Empty<ChatMessage>());
        if (!newMessages.Any()) return;

        var repo = _serviceProvider.GetRequiredService<IRepository<Session>>();

        // 加载 DDD 聚合根
        var session = await repo.GetByIdAsync(_sessionSoreState.SessionId, cancellationToken);
        if (session == null) return;

        var hasNewMessage = false;
        foreach (var msg in newMessages)
        {
            var roleStr = msg.Role.ToString().ToLower();
            var msgType = roleStr switch
            {
                "user" => MessageType.User,
                "assistant" => MessageType.Assistant,
                "system" => MessageType.System,
                _ => MessageType.Assistant
            };

            if (string.IsNullOrWhiteSpace(msg.Text)) continue;

            session.AddMessage(msg.Text, msgType);
            hasNewMessage = true;
        }

        if (hasNewMessage)
        {
            repo.Update(session);
            await repo.SaveChangesAsync(cancellationToken);
        }
    }
}