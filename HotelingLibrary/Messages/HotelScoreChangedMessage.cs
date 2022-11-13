namespace HotelingLibrary.Messages
{
    internal class HotelScoreChangedMessage : MessageBase
    {
        public double NewScore { get; set; }
    }
}
