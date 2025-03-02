namespace Trellix.Repositories.Crud;

public interface IUnitOfWork
{
    IAttachmentRepository Attachments { get; }
    Task CompleteAsync();
}