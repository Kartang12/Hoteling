namespace HotelApi.Domain
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public int Number { get; set; }
        public bool Wifi { get; set; }
        public double Square { get; set; }
    }
}
