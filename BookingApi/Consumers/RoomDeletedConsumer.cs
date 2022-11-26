using BookingApi.Services;
using HotelingLibrary.Messages;
using MassTransit;

namespace BookingApi.Consumers
{
    public class RoomDeletedConsumer : IConsumer<RoomDeletedMessage>
    {
        private readonly IBookingService _service;

        public RoomDeletedConsumer(IBookingService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<RoomDeletedMessage> consumeContext)
        {
            await _service.ConsumeRoomDeletedMessage(consumeContext); 
        }
    }
}
