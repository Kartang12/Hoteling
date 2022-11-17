using BookingApi.DbContext;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class RoomChangedConsumer : IConsumer<RoomDataChangedMessage>
    {
        private readonly BookingContext _context;
        public RoomChangedConsumer(BookingContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<RoomDataChangedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.RoomId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].RoomNumber = consumeContext.Message.Number;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
