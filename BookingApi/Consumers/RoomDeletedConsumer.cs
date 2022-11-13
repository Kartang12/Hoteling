using BookingApi.DbContext;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedMessage>
    {
        private readonly BookingContext _context;

        public RoomDeletedConsumer(BookingContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<RoomDeletedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.RoomId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].RoomNumber = -1;
                bookings[i].RoomId = Guid.Empty;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
