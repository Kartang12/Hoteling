using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.DbContext;
using HotelApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services
{
    public class RoomService
    {
        private readonly HotelingContext _context;
        private readonly IMapper _mapper;

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
            var result = await _context.Rooms.AddAsync(newRoom);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            _context.Rooms.Remove(toDelete);
        }

        public async Task<Room> UpdateAsync(Room toUpdate)
        {
            _context.Rooms.Update(toUpdate);
            return toUpdate;
        }
    }
}
