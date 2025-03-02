namespace Trellix.DataAccess.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trellix.Core.Models;
using Trellix.DataAccess.Exceptions;
using Trellix.Repositories.Crud;

public class BaseRepository<T>(TrellixDBContext dbContext, ILogger logger) : IBaseRepository<T> where T : BaseModel
{
    protected TrellixDBContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    internal DbSet<T> _dbSet = dbContext.Set<T>() ?? throw new ArgumentNullException(nameof(dbContext));
    public readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task AddAsync(T item)
    {
        try
        {
            item.CreatedOn = DateTime.UtcNow;
            item.IsActive = true;
            item.ModifiedOn = DateTime.UtcNow;

            await _dbSet.AddAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR: unable to add new entity");

            throw new DataAccessException("ERROR: unable to add new entity", ex);
        }
    }

    public Task RemoveAsync(T item)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        try
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR: unable to get all enties");

            throw new DataAccessException("ERROR: unable to get all enties", ex);
        }
    }

    public Task UpdateAsync(T item)
    {
        throw new NotImplementedException();
    }
}