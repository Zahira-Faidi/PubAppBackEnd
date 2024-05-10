namespace MongoDBIdentity.Utilities
{
    public class MongoDbConfig
    {
        public string Name { get; init; }
        public string Host { get; init; }
        public int? Port { get; init; }
        public string ConnectionString => Port.HasValue ? $"mongodb://{Host}:{Port}" : $"mongodb{Host}";
    }
}
