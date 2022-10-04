using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.DbContext;
using HotelApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services
{
    public interface IHotelService
    {
        Task<Hotel> CreateAsync(CreateHotelRequest request);
        Task DeleteAsync(Guid id);
        Task<Hotel> GetByIdAsync(Guid id);
        Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Hotel> UpdateAsync(Hotel toUpdate);
    }

    public class HotelService : IHotelService
    {
        private readonly HotelingContext _context;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Hotels.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(Guid id)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Hotel> CreateAsync(CreateHotelRequest request)
        {
            var newHotel = _mapper.Map<Hotel>(request);
            newHotel.Id = Guid.NewGuid();
            var result = await _context.Hotels.AddAsync(newHotel);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
            _context.Hotels.Remove(toDelete);
        }

        public async Task<Hotel> UpdateAsync(Hotel toUpdate)
        {
            var result = _context.Hotels.Update(toUpdate);
            return result.Entity;
        }
    }
}
