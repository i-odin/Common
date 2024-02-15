namespace Common.EFCore.Models;

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