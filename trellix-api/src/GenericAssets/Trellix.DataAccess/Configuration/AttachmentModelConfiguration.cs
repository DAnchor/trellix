namespace Trellix.DataAccess.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trellix.Core.Models;

public class AttachmentModelConfiguration : IEntityTypeConfiguration<AttachmentModel>
{
    public void Configure(EntityTypeBuilder<AttachmentModel> builder)
    {
        // properties
        builder.HasKey(x => x.Id);
        builder.Property<string>(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property<byte[]>(x => x.Data).IsRequired();
        builder.Property<DateTime>(x => x.CreatedOn).IsRequired();
        builder.Property<DateTime>(x => x.ModifiedOn).IsRequired();
        builder.Property<bool>(x => x.IsActive).IsRequired();

        // entity
        builder.ToTable("Attachments");
    }
}
