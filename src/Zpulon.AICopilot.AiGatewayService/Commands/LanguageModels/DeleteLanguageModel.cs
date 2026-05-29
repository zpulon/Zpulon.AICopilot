using System;
using System.Threading;
using System.Threading.Tasks;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.LanguageModel;
using Zpulon.AICopilot.Services.Common.Attributes;
using Zpulon.AICopilot.SharedKernel.Messaging;
using Zpulon.AICopilot.SharedKernel.Repository;
using Zpulon.AICopilot.SharedKernel.Result;

namespace Zpulon.AICopilot.AiGatewayService.Commands.LanguageModels;

[AuthorizeRequirement("AiGateway.DeleteLanguageModel")]
public record DeleteLanguageModelCommand(Guid Id) : ICommand<Result>;

public class DeleteLanguageModelCommandHandler(IRepository<LanguageModel> repo)
    : ICommandHandler<DeleteLanguageModelCommand, Result>
{
    public async Task<Result> Handle(DeleteLanguageModelCommand request, CancellationToken cancellationToken)
    {
        var result = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (result == null) return Result.Success();

        repo.Delete(result);
        await repo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}