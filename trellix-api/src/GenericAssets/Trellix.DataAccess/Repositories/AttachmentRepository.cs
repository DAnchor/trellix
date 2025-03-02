namespace Trellix.DataAccess.Repositories.Crud;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trellix.Core.Models;
using Trellix.DataAccess;
using Trellix.DataAccess.Exceptions;
using Trellix.DataAccess.Repositories;
using Trellix.Repositories.Crud;

public class AttachmentRepository(TrellixDBContext context, ILogger logger)
    : BaseRepository<AttachmentModel>(context, logger), IAttachmentRepository
{
    public async Task<AttachmentModel> GetAttachmentByIdAsync(Guid id)
    {
        try
        {
            return await _dbSet
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            
            throw new DataAccessException(ex.Message, ex);
        }
    }
}
