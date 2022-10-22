namespace HotelingLibrary.Messages
{
    public abstract class EventBase
    {
        public OperationTypeEnum OperationType { get; set; }
        public Guid EntityId { get; set; }
    }
}
