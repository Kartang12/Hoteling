using AuthorizationApi.Services;
using HotelingLibrary.Messages;
using MassTransit;

namespace AuthorizationApi.Consumeres
{
    public class UserChangedConsumer : IConsumer<UserDataChangedMessage>
    {
        private readonly IAuthService _service;

        public async Task Consume(ConsumeContext<UserDataChangedMessage> consumeContext)
        {
            await _service.ConsumeUserChangedMessage(consumeContext);
        }
    }
}
