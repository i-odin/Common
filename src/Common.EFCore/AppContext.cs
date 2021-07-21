using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Common.EFCore.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.EFCore
{
    public class AppContext : DbContext
    {
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
                entry?.SetTimeStamp();
        }
    }
}
