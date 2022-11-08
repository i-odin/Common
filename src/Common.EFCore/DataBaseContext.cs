using Microsoft.EntityFrameworkCore;

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
    }
}