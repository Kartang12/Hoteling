using BookingApi.DbContext;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class HotelChangedConsumer : IConsumer<HotelDataChangedMessage>
    {
        private readonly BookingContext _context;
        public HotelChangedConsumer(BookingContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].HotelName = consumeContext.Message.Name;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
