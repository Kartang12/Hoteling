using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.DbContext;
using HotelApi.Domain;
using HotelingLibrary.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services
{
    public interface IHotelService
    {
        Task<Hotel> CreateAsync(CreateHotelRequest request);
        Task DeleteAsync(Guid id);
        Task<Hotel> GetByIdAsync(Guid id);
        Task<IEnumerable<Hotel>> GetAll();
        Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Hotel> UpdateAsync(Hotel toUpdate);
    }

    public class HotelService : IHotelService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly HotelingContext _context;
        private readonly IMapper _mapper;

        public HotelService(HotelingContext context,
            IMapper mapper,
            IBus bus,
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<Hotel>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Hotels.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(Guid id)
        {
            return await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetAll()
        {
            return await _context.Hotels.ToListAsync();
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
            var deletedEntity = _context.Hotels.Remove(toDelete);
            var deleteMessage = _mapper.Map<HotelDeletedMessage>(deletedEntity.Entity);
            await _publishEndpoint.Publish(deleteMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> UpdateAsync(Hotel toUpdate)
        {
            var result = _context.Hotels.Update(toUpdate);
            var updateMessage = _mapper.Map<HotelDataChangedMessage>(result.Entity);
            await _publishEndpoint.Publish(updateMessage);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
