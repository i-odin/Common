using System;
using System.Collections.Generic;

namespace Common.Core.Model
{
    public abstract class HasIdBase<TKey> : IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        object IHasId.Id => Id;

        public override bool Equals(object obj)
        {
            if (!(obj is HasIdBase<TKey> other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(HasIdBase<TKey> a, HasIdBase<TKey> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(HasIdBase<TKey> a, HasIdBase<TKey> b) => !(a == b);

        public override int GetHashCode() => EqualityComparer<TKey>.Default.GetHashCode(Id);
    }
}
