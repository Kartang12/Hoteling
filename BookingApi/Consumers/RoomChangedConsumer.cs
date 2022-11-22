using BookingApi.Services;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class RoomChangedConsumer : IConsumer<RoomDataChangedMessage>
    {
        private readonly IBookingService _service;

        public RoomChangedConsumer(IBookingService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<RoomDataChangedMessage> consumeContext)
        {
            await _service.ConsumeRoomChangedMessage(consumeContext);
        }
    }
}
