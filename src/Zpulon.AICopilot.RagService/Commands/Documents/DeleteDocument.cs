using Zpulon.AICopilot.Core.Rag.Aggregates.KnowledgeBase;
using Zpulon.AICopilot.Services.Common.Attributes;
using Zpulon.AICopilot.SharedKernel.Messaging;
using Zpulon.AICopilot.SharedKernel.Repository;
using Zpulon.AICopilot.SharedKernel.Result;

namespace Zpulon.AICopilot.RagService.Commands.Documents;

[AuthorizeRequirement("Rag.DeleteDocument")]
public record DeleteDocumentCommand(Guid KnowledgeBaseId, int DocumentId) : ICommand<Result>;

public class DeleteDocumentCommandHandler(IRepository<KnowledgeBase> repo)
    : ICommandHandler<DeleteDocumentCommand, Result>
{
    public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var result = await repo.GetAsync(kb => kb.Id == request.KnowledgeBaseId,
            [k => k.Documents.Where(d => d.Id == request.DocumentId)], cancellationToken);
        if (result == null) return Result.Success();

        result.RemoveDocument(request.DocumentId);
        await repo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}