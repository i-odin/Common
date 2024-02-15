namespace Common.EFCore.Models;
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