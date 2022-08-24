using System.Diagnostics.CodeAnalysis;

namespace Common.Core.Models;

public class Entity : IHasId<Guid>, ITimeStamp, IDeleted, IEquatable<Entity>
{
    object IHasId.Id => Id;
    public Guid Id { get; init; }
    public DateTime Timestamp { get; set; }
    public bool Deleted { get; set; }

    public bool Equals([MaybeNull] Entity? other) => ((IHasId<Guid>)this).Equals(other) && 
                                                     ((ITimeStamp)this).Equals(other) &&
                                                     ((IDeleted)this).Equals(other);
    public override bool Equals([MaybeNull] object? obj) => Equals(obj as Entity);
    public override int GetHashCode() => HashCode.Combine(Id, Timestamp, Deleted);

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b) => !(a == b);
    
    public static Entity Create() => new Entity { Id = NewId(), Timestamp = DateTime.UtcNow };
    public static Guid NewId() => Guid.NewGuid();
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

public interface ITimeStamp : IEquatable<ITimeStamp>
{
    DateTime Timestamp { get; set; }

    bool IEquatable<ITimeStamp>.Equals(ITimeStamp? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<DateTime>.Default.Equals(Timestamp, other.Timestamp);
    }
}

public interface IDeleted : IEquatable<IDeleted>
{
    public bool Deleted { get; set; }

    bool IEquatable<IDeleted>.Equals(IDeleted? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<bool>.Default.Equals(Deleted, other.Deleted);
    }
}