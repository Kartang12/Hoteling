using AutoMapper;
using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReviewApi.Contracts.Requests;
using ReviewApi.DbContext;
using ReviewApi.Domain;

namespace ReviewApi.Services
{
    public interface IReviewService
    {
        Task<Review> CreateAsync(CreateReviewRequest request);
        Task DeleteAsync(Guid id);
        Task<Review> GetByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Review> UpdateAsync(Review toUpdate);
        Task ConsumeHotelChangedMessage(ConsumeContext<HotelDataChangedMessage> consumeContext);
        Task ConsumeHotelDeletedMessage(ConsumeContext<HotelDeletedMessage> consumeContext);
    }

    public class ReviewService : IReviewService
    {
        readonly IPublishEndpoint _publishEndpoint;
        readonly ReviewsContext _context;
        private readonly IMapper _mapper;

        public ReviewService(ReviewsContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<IEnumerable<Review>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Reviews.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> CreateAsync(CreateReviewRequest request)
        {
            var newReview = _mapper.Map<Review>(request);
            newReview.Id = Guid.NewGuid();
            newReview.Date = DateTime.Now;
            var result = await _context.Reviews.AddAsync(newReview);
            var addMessage = _mapper.Map<ReviewAddedMessage>(result);
            await _publishEndpoint.Publish(addMessage);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            _context.Reviews.Remove(toDelete);
        }

        public async Task<Review> UpdateAsync(Review toUpdate)
        {
            var result = _context.Reviews.Update(toUpdate);
            return result.Entity;
        }

        public async Task ConsumeHotelChangedMessage(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            var reviews = _context.Reviews.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < reviews.Count(); i++)
            {
                reviews[i].HotelName = consumeContext.Message.Name;
            }
            _context.Reviews.UpdateRange(reviews);
            await _context.SaveChangesAsync();
        }

        public async Task ConsumeHotelDeletedMessage(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            var reviews = _context.Reviews.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            _context.Reviews.RemoveRange(reviews);
            var ids = reviews.Select(x => x.Id).ToList();
            await _publishEndpoint.Publish(new ReviewDeletedMessage() { UsersDeletedReviews = ids });
            await _context.SaveChangesAsync();
        }
    }
}
