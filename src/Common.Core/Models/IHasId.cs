using System;

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
}
