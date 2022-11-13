namespace HotelingLibrary.Messages
{
    public abstract class MessageBase
    {
        public OperationTypeMessage OperationType { get; set; }
        public Guid EntityId { get; set; }
    }
}
