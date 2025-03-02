namespace Trellix.DataAccess;

using Microsoft.EntityFrameworkCore;
using Trellix.Core.Models;
using Trellix.DataAccess.Configuration;

public class TrellixDBContext
(
    DbContextOptions<TrellixDBContext> options
) : DbContext(options)
{
    public virtual DbSet<AttachmentModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SetTableProperties(modelBuilder);
    }

    private void SetTableProperties(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AttachmentModelConfiguration());
    }
}