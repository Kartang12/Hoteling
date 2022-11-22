using HotelingLibrary.Messages;
using MassTransit;
using UserApi.Services;

namespace UserApi.Consumers
{
    public class ReviewDeletedConsummer : IConsumer<ReviewDeletedMessage>
    {
        private readonly IUserService _service;

        public ReviewDeletedConsummer(IUserService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<ReviewDeletedMessage> consumeContext)
        {
            await _service.ConsumeReviewDeletedMessage(consumeContext);
        }
    }
}
