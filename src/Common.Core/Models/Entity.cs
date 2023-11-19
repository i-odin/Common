using System.Diagnostics.CodeAnalysis;

namespace Common.Core.Models;

public abstract class Entity : IHasId<Guid>, IHasTimestamp, IHasDeleted, IEquatable<Entity>
{
    object IHasId.Id => Id;
    public Guid Id { get; init; }
    public DateTime Timestamp { get; init; }
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
    public static TEntity Create<TEntity>(Action<TEntity>? setting = null)
        where TEntity : Entity, new()
    {
        var entity = new TEntity {
            Id = NewId(),
            Timestamp = DateTime.UtcNow
        };

        setting?.Invoke(entity);
        return entity;
    }
}

public interface IHasId
{
    object Id { get; }
}

public interface IHasId<TKey> : IHasId, IEquatable<IHasId<TKey>>
{
    new TKey Id { get; }

    bool IEquatable<IHasId<TKey>>.Equals(IHasId<TKey>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }
}

public interface IHasTimestamp : IEquatable<IHasTimestamp>
{
    DateTime Timestamp { get; }

    bool IEquatable<IHasTimestamp>.Equals(IHasTimestamp? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<DateTime>.Default.Equals(Timestamp, other.Timestamp);
    }
}

public interface IHasDeleted : IEquatable<IHasDeleted>
{
    public bool Deleted { get; set; }

    bool IEquatable<IHasDeleted>.Equals(IHasDeleted? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<bool>.Default.Equals(Deleted, other.Deleted);
    }
}