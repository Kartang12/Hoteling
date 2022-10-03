using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Contracts.Requests;
using UserApi.DbContext;
using UserApi.Domain;

namespace UserApi.Services
{
    public class UserService
    {
        readonly UsersContext _context;
        readonly IMapper _mapper;

        public UserService(IMapper mapper, UsersContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserData>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<UserData> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserData> CreateAsync(CreateUserDataRequest request)
        {
            var newHotel = _mapper.Map<UserData>(request);
            newHotel.Id = Guid.NewGuid();
            var result = await _context.Users.AddAsync(newHotel);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(toDelete);
        }

        public async Task<UserData> UpdateAsync(UserData toUpdate)
        {
            var result = _context.Users.Update(toUpdate);
            return result.Entity;
        }
    }
}
