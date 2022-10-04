using BookingApi.Contracts.Requests;
using BookingApi.Contracts.Responses;
using BookingApi.DbContext;
using BookingApi.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task<Booking> GetById(Guid id);

        Task<BookingResponse> CreateAsync(BookingRequest request);

        Task DeleteAsync(Guid id);

        Task<Booking> UpdateAsync(Booking toUpdate);
    }

    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public BookingService(IMapper mapper, BookingContext context = null)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Bookings.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        
        public async Task<Booking> GetById(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BookingResponse> CreateAsync(BookingRequest request)
        {
            var newBooking = _mapper.Map<Booking>(request);
            newBooking.Id = Guid.NewGuid();
            var completeBooking = await _context.Bookings.AddAsync(newBooking);
            return _mapper.Map<BookingResponse>(completeBooking);
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            _context.Bookings.Remove(toDelete);
        }

        public async Task<Booking> UpdateAsync(Booking toUpdate)
        {
            _context.Bookings.Update(toUpdate);
            return toUpdate;
        }
    }
}
