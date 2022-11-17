using AuthorizationApi.Context;
using AuthorizationApi.Domain;
using HotelingLibrary.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.Consumeres
{
    public class UserChangedConsumer : IConsumer<UserDataChangedMessage>
    {
        private readonly AuthDbContext _context;

        public UserChangedConsumer(AuthDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserDataChangedMessage> consumeContext)
        {
            var user = await _context.Users.FirstAsync<User>(x => x.Id == consumeContext.Message.EntityId);
            user.Email = consumeContext.Message.Email;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
