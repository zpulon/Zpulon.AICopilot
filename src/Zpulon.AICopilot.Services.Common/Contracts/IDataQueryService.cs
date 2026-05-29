using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.ConversationTemplate;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.LanguageModel;
using Zpulon.AICopilot.Core.AiGateway.Aggregates.Sessions;
using Zpulon.AICopilot.Core.McpServer.Aggregates.McpServerInfo;
using Zpulon.AICopilot.Core.Rag.Aggregates.EmbeddingModel;
using Zpulon.AICopilot.Core.Rag.Aggregates.KnowledgeBase;

namespace Zpulon.AICopilot.Services.Common.Contracts;

public interface IDataQueryService
{
    public IQueryable<ConversationTemplate> ConversationTemplates { get; }

    public IQueryable<LanguageModel> LanguageModels { get; }

    public IQueryable<Session> Sessions { get; }

    public IQueryable<Message> Messages { get; }
    
    public IQueryable<EmbeddingModel> EmbeddingModels { get; }
    
    public IQueryable<KnowledgeBase> KnowledgeBases { get; }
    
    public IQueryable<Document> Documents { get; }
    
    public IQueryable<DocumentChunk> DocumentChunks { get; }
    
    public IQueryable<BusinessDatabase> BusinessDatabases { get; }
    
    public IQueryable<McpServerInfo> McpServerInfos { get; }

    Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class;

    Task<IList<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class;

    Task<bool> AnyAsync<T>(IQueryable<T> queryable) where T : class;
}