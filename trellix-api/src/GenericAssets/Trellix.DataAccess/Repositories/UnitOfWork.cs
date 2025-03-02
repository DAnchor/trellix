namespace Trellix.DataAccess.Repositories.Crud;

using Microsoft.Extensions.Logging;
using Trellix.DataAccess;
using Trellix.Repositories.Crud;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TrellixDBContext _dbContext;
    private readonly ILogger _logger;
    public IAttachmentRepository Attachments { get; private set; }

    public UnitOfWork
    (
        TrellixDBContext dbContext, 
        ILoggerFactory loggerFactory
    )
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = loggerFactory.CreateLogger("Logs") ?? throw new ArgumentNullException(nameof(loggerFactory));

        Attachments = new AttachmentRepository(dbContext, _logger);
    }

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync() => await _dbContext.DisposeAsync();

    public void Dispose() => _dbContext.Dispose();
}