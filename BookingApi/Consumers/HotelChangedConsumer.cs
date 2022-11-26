using BookingApi.Services;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class HotelChangedConsumer : IConsumer<HotelDataChangedMessage>
    {
        private readonly IBookingService _service;

        public HotelChangedConsumer(IBookingService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            await _service.ConsumeHotelDataChangedMessage(consumeContext);
        }
    }
}
