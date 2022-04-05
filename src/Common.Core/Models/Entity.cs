using System.Diagnostics.CodeAnalysis;

namespace Common.Core.Models;

public class Entity : IHasId<Guid>, ITimeStamp, IEquatable<Entity>
{
    object IHasId.Id => Id;
    public Guid Id { get; init; }
    public DateTime Timestamp { get; set; }

    public override bool Equals([MaybeNull] object? obj)
    {
        var entity = obj as Entity;
        return Equals(entity);
    }

    public bool Equals([MaybeNull] Entity? other)
    {
        var hasId = other as IHasId<Guid>;
        var timeStamp = other as ITimeStamp;
        return ((IHasId<Guid>)this).Equals(hasId) && ((ITimeStamp)this).Equals(timeStamp);
    }

    public override int GetHashCode() => HashCode.Combine(Id, Timestamp);

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b) => !(a == b);
    
    public static Guid NewId() => Guid.NewGuid();
}

public interface IHasId
{
    object Id { get; }
}

public interface IHasId<TKey> : IHasId
{
    new TKey Id { get; }

    bool Equals(IHasId<TKey>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }
}

public interface ITimeStamp
{
    DateTime Timestamp { get; set; }

    bool Equals(ITimeStamp? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<DateTime>.Default.Equals(Timestamp, other.Timestamp);
    }
}