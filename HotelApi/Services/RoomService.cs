using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.DbContext;
using HotelApi.Domain;
using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using static MassTransit.ValidationResultExtensions;

namespace HotelApi.Services
{
    public interface IRoomService
    {
        Task<Room> CreateAsync(CreateRoomRequest request);
        Task DeleteAsync(Guid id);
        Task<Room> GetByIdAsync(Guid id);
        Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Room> UpdateAsync(Room toUpdate);
    }

    public class RoomService : IRoomService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly HotelingContext _context;

        public RoomService(HotelingContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }


        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Rooms.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Room> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Room> CreateAsync(CreateRoomRequest request)
        {
            var newRoom = _mapper.Map<Room>(request);
            newRoom.Id = Guid.NewGuid();
            var result = await _context.Rooms.AddAsync(newRoom);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            var deletedEntity = _context.Rooms.Remove(toDelete);

            var deleteMessage = _mapper.Map<RoomDeletedMessage>(deletedEntity.Entity);
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{QueuesUrls.Booking_RoomDeleted}"));
            await endpoint.Send(deleteMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> UpdateAsync(Room toUpdate)
        {
            var result = _context.Rooms.Update(toUpdate);
            var updateEvent = _mapper.Map<RoomDataChangedMessage>(result.Entity);
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{QueuesUrls.Booking_RoomChanged}"));
            await endpoint.Send(updateEvent);
            await _context.SaveChangesAsync();
            return toUpdate;
        }
    }
}
