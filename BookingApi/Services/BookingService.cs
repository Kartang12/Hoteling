using BookingApi.Contracts.Requests;
using BookingApi.Contracts.Responses;
using BookingApi.DbContext;
using BookingApi.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using HotelingLibrary.Messages;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        void Get();
        Task<IEnumerable<Booking>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Booking> GetById(Guid id);
        Task<IEnumerable<Booking>> GetAll();
        Task<BookingResponse> CreateAsync(BookingRequest request);
        Task DeleteAsync(Guid id);
        Task<Booking> UpdateAsync(Booking toUpdate);
        Task ConsumeHotelDataChangedMessage(ConsumeContext<HotelDataChangedMessage> consumeContext);
        Task ConsumeHotelDeletedMessage(ConsumeContext<HotelDeletedMessage> consumeContext);
        Task ConsumeRoomChangedMessage(ConsumeContext<RoomDataChangedMessage> consumeContext);
        Task ConsumeRoomDeletedMessage(ConsumeContext<RoomDeletedMessage> consumeContext);
    }

    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public void Get()
        {
            //_publishEndpoint.Publish(new HotelDataChangedMessage()
            //{
            //    Name = "test"
            //});
        }

        public BookingService(IMapper mapper, BookingContext context = null, IPublishEndpoint publishEndpoint = null)
        {
            _mapper = mapper;
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<Booking>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Bookings.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        
        public async Task<Booking> GetById(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<BookingResponse> CreateAsync(BookingRequest request)
        {
            var newBooking = _mapper.Map<Booking>(request);
            newBooking.Id = Guid.NewGuid();
            var completeBooking = await _context.Bookings.AddAsync(newBooking);
            return _mapper.Map<BookingResponse>(completeBooking.Entity);
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
        
        public async Task ConsumeHotelDataChangedMessage(ConsumeContext<HotelDataChangedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].HotelName = consumeContext.Message.Name;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }

        public async Task ConsumeHotelDeletedMessage(ConsumeContext<HotelDeletedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.HotelId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].HotelName = "Hotel deleted";
                bookings[i].HotelId = Guid.Empty;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }

        public async Task ConsumeRoomChangedMessage(ConsumeContext<RoomDataChangedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.RoomId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].RoomNumber = consumeContext.Message.Number;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }

        public async Task ConsumeRoomDeletedMessage(ConsumeContext<RoomDeletedMessage> consumeContext)
        {
            var bookings = _context.Bookings.Where(x => x.RoomId == consumeContext.Message.EntityId).ToList();
            for (int i = 0; i < bookings.Count(); i++)
            {
                bookings[i].RoomNumber = -1;
                bookings[i].RoomId = Guid.Empty;
            }
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }
    }
}
