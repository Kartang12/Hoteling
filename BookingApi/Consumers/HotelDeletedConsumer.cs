using BookingApi.DbContext;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class HotelDeletedConsumer : IConsumer<HotelDeletedMessage>
    {
        private readonly BookingContext _context;
        
        public HotelDeletedConsumer(BookingContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].HotelName = "Hotel deleted";
                bookings[i].HotelId = Guid.Empty;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
