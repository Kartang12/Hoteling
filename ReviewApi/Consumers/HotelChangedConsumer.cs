using HotelingLibrary.Messages;
using MassTransit;
using ReviewApi.DbContext;

namespace ReviewApi.Consumers
{
    public class HotelChangedConsumer: IConsumer<HotelDataChangedMessage>
    {
        private readonly ReviewsContext _context;
        public HotelChangedConsumer(ReviewsContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            var reviews = _context.Reviews.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < reviews.Count(); i++)
            {
                reviews[i].HotelName = consumeContext.Message.Name;
            }
            _context.Reviews.UpdateRange(reviews);
            await _context.SaveChangesAsync();
        }
    }
}
