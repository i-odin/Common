using Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.EFCore.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> buider)
    {
        buider.HasKey(o => o.Id);
        //buider.HasIndex(o => o.Id).IsUnique();
        buider.Property(o => o.Timestamp).IsRequired();
        buider.Property(o => o.Deleted).IsRequired();
    }
}