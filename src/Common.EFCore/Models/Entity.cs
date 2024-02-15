using System.Diagnostics.CodeAnalysis;

namespace Common.EFCore.Models;

public abstract class Entity : IHasId<Guid>, IHasTimestamp, IHasDeleted, IEquatable<Entity>
{
    object IHasId.Id => Id;
    public Guid Id { get; init; }
    public DateTimeOffset Timestamp { get; init; }
    public bool Deleted { get; set; }

    public bool Equals([MaybeNull] Entity? other) => ((IHasId<Guid>)this).Equals(other) &&
                                                     ((IHasTimestamp)this).Equals(other) &&
                                                     ((IHasDeleted)this).Equals(other);
    public override bool Equals([MaybeNull] object? obj) => Equals(obj as Entity);
    public override int GetHashCode() => HashCode.Combine(Id, Timestamp, Deleted);

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b) => !(a == b);
    public static Guid NewId() => Guid.NewGuid();
    public static TEntity Make<TEntity>(Action<TEntity>? setting = null)
        where TEntity : Entity, new()
    {
        var entity = new TEntity
        {
            Id = NewId(),
            Timestamp = DateTimeOffset.UtcNow
        };

        setting?.Invoke(entity);
        return entity;
    }
}

