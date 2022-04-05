using System.Diagnostics.CodeAnalysis;
using Common.Core.Helpers;

namespace Common.Core.Structs;

public readonly struct KeyValueString : IEquatable<KeyValueString>
{
    public KeyValueString(
        [NotNull] string key,
        [NotNull] string value,
        char kvSeparator = Symbol.Equal,
        char separator = Symbol.Semicolon)
    {
        Key = key;
        Value = value;
        KeyValueSeparator = kvSeparator;
        Separator = separator;
    }

    public string Key { get; }
    public string Value { get; }
    public char KeyValueSeparator { get; }
    public char Separator { get; }

    public override string ToString() =>
        string.Concat(Key, KeyValueSeparator.ToString(), Value,  Separator.ToString());

    public bool Equals(KeyValueString other) => 
        (Key, KeyValueSeparator, Value, Separator).Equals((other.Key, other.KeyValueSeparator, other.Value, other.Separator));

    public override bool Equals(object? obj) => 
        obj is KeyValueString other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(Key, KeyValueSeparator, Value, Separator);

    public static bool operator ==(KeyValueString left, KeyValueString right) => 
        left.Equals(right);

    public static bool operator !=(KeyValueString left, KeyValueString right) => 
        !(left == right);
}