using AutoMapper;
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
        Task<IEnumerable<Review>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Review> UpdateAsync(Review toUpdate);
    }

    public class ReviewService : IReviewService
    {
        readonly ReviewsContext _context;
        private readonly IMapper _mapper;

        public ReviewService(ReviewsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<Review>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Reviews.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Review> CreateAsync(CreateReviewRequest request)
        {
            var newReview = _mapper.Map<Review>(request);
            newReview.Id = Guid.NewGuid();
            newReview.Date = DateTime.Now;
            var result = await _context.Reviews.AddAsync(newReview);
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
    }
}
