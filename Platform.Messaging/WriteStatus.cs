namespace Platform.Messaging
{
    public enum WriteStatus
    {
        Success,
        WrongExpectedVersion,
        AlreadyProcessed,
        Error
    }
}