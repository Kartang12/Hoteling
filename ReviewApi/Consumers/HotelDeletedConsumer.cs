using HotelingLibrary.Messages;
using MassTransit;
using ReviewApi.Services;

namespace ReviewApi.Consumers
{
    public class HotelDeletedConsumer : IConsumer<HotelDeletedMessage>
    {
        private readonly IReviewService _service;

        public HotelDeletedConsumer(IReviewService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            await _service.ConsumeHotelDeletedMessage(consumeContext);
        }
    }
}
