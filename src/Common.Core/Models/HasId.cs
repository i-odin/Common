using System;
using System.Collections.Generic;

namespace Common.Core.Models
{
    public interface IHasId
    {
        object Id { get; }
    }

    public interface IHasId<out TKey> : IHasId
        where TKey : IEquatable<TKey>
    {
        new TKey Id { get; }
    }

    public abstract class HasId<TKey> : IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        object IHasId.Id => Id;
        
        public TKey Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is HasId<TKey> other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(HasId<TKey>? a, HasId<TKey>? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(HasId<TKey>? a, HasId<TKey>? b) => !(a == b);

        public override int GetHashCode() => EqualityComparer<TKey>.Default.GetHashCode(Id);
    }
}
