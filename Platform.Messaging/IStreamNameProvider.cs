namespace Platform.Messaging
{
    public interface IStreamNameProvider
    {
        string GetStreamName(string type, string aggregateId);
    }
}