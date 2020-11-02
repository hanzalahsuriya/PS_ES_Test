namespace Platform.Messaging
{
    public class StreamNameProvider : IStreamNameProvider
    {
        public string GetStreamName(string typeName, string aggregateId)
        {
            var streamName = $"{typeName}-{aggregateId}";
            return streamName;
        }
    }
}