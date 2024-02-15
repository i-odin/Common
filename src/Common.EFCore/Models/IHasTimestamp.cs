namespace Common.EFCore.Models;

public interface IHasTimestamp : IEquatable<IHasTimestamp>
{
    DateTimeOffset Timestamp { get; }

    bool IEquatable<IHasTimestamp>.Equals(IHasTimestamp? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<DateTimeOffset>.Default.Equals(Timestamp, other.Timestamp);
    }
}
