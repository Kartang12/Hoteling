using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
using MassTransit.Initializers;
using ReviewApi.DbContext;

namespace ReviewApi.Consumers
{
    public class HotelDeletedConsumer : IConsumer<HotelDeletedMessage>
    {
        private readonly ReviewsContext _context;
        private readonly IBus _bus;

        public HotelDeletedConsumer(ReviewsContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            var reviews = _context.Reviews.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            _context.Reviews.RemoveRange(reviews);
            var ids = reviews.Select(x => x.Id).ToList();
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue{QueuesUrls.User_ReviewsDeleted}"));
            endpoint.Send(new ReviewDeletedMessage() { UsersDeletedReviews = ids });
            await _context.SaveChangesAsync();
        }
    }
}
