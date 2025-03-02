namespace Trellix.Repositories.Crud;

using Trellix.Core.Models;

public interface IAttachmentRepository : IBaseRepository<AttachmentModel> 
{
    Task<AttachmentModel> GetAttachmentByIdAsync(Guid id);
}