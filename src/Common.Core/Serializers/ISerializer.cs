namespace Common.Core.Serializers
{
    public interface ISerializer
    {
        public string Serialize<T>(T source);
        public T Deserialize<T>(string source);
    }
}
