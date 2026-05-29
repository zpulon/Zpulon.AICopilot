using System;
using System.Threading;
using System.Threading.Tasks;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.Sessions;
using Zpulon.AICopilot.Services.Common.Attributes;
using Zpulon.AICopilot.SharedKernel.Messaging;
using Zpulon.AICopilot.SharedKernel.Repository;
using Zpulon.AICopilot.SharedKernel.Result;

namespace Zpulon.AICopilot.AiGatewayService.Commands.Sessions;

[AuthorizeRequirement("AiGateway.DeleteSession")]
public record DeleteSessionCommand(Guid Id) : ICommand<Result>;

public class DeleteSessionCommandHandler(IRepository<Session> repo)
    : ICommandHandler<DeleteSessionCommand, Result>
{
    public async Task<Result> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var result = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (result == null) return Result.Success();

        repo.Delete(result);
        await repo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}