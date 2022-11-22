using BookingApi.Services;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class HotelDeletedConsumer : IConsumer<HotelDeletedMessage>
    {
        private readonly IBookingService _bookingService;

        public HotelDeletedConsumer(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task Consume(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            await _bookingService.ConsumeHotelDeletedMessage(consumeContext);
        }
    }
}
