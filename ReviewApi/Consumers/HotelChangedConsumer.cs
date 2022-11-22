using HotelingLibrary.Messages;
using MassTransit;
using ReviewApi.DbContext;
using ReviewApi.Services;

namespace ReviewApi.Consumers
{
    public class HotelChangedConsumer: IConsumer<HotelDataChangedMessage>
    {
        private readonly IReviewService _service;

        public HotelChangedConsumer(IReviewService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            await _service.ConsumeHotelChangedMessage(consumeContext);
        }
    }
}
