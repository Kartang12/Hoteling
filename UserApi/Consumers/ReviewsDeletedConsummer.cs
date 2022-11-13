using HotelingLibrary.Messages;
using MassTransit;
using UserApi.DbContext;
using UserApi.Domain;

namespace UserApi.Consumers
{
    public class ReviewsDeletedConsummer : IConsumer<ReviewsDeletedMessage>
    {
        private readonly UsersContext _context;

        public ReviewsDeletedConsummer(UsersContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ReviewsDeletedMessage> consumeContext)
        {
            List<UserData> updatedUsers = new List<UserData>();
            consumeContext.Message.UsersDeletedReviews.ForEach(
                x =>
                {
                    var user = _context.Users.First(u => u.UserId == x);
                    user.ReviewsAmount--;
                    updatedUsers.Add(user);
                });
            _context.Users.UpdateRange(updatedUsers);
            await _context.SaveChangesAsync();
        }
    }
}
