using Microsoft.EntityFrameworkCore;
using Common.EFCore.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.EFCore;

public class DataBaseContext<TContext> : DbContext
    where TContext : DbContext
{
    public DataBaseContext(DbContextOptions<TContext> options) : base(options)
    {

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    protected virtual void OnBeforeSaving()
    {
        foreach (EntityEntry? entry in ChangeTracker.Entries()) 
            entry?.SetTimestamp();
    }
}