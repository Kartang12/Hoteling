using BookingApi.Contracts.Requests;
using BookingApi.Contracts.Responses;
using BookingApi.DbContext;
using BookingApi.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Services
{
    public class BookingService
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<Booking>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _context.Bookings.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        
        public async Task<Booking> GetById(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BookingResponse> Book(BookingRequest request)
        {
            var newBooking = _mapper.Map<Booking>(request);
            var completeBooking = await _context.Bookings.AddAsync(newBooking);
            return _mapper.Map<BookingResponse>(completeBooking);
        }

        public async Task UnBook(Guid id)
        {
            var toDelete = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            _context.Bookings.Remove(toDelete);
        }

        public async Task<Booking> Update(Booking toUpdate)
        {
            _context.Bookings.Update(toUpdate);
            return toUpdate;
        }
    }
}
