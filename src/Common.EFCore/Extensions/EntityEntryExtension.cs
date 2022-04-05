using Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.EFCore.Extensions;

public static class EntityEntryExtension
{
    public static void SetTimeStamp(this EntityEntry entry)
    {
        if (entry.Entity is ITimeStamp timeStamp && entry.State is EntityState.Modified or EntityState.Added)
            timeStamp.Timestamp = DateTime.UtcNow;
    }
}