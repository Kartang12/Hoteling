using AutoMapper;
using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Contracts.Requests;
using UserApi.DbContext;
using UserApi.Domain;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<UserData> CreateAsync(CreateUserDataRequest request);
        Task<UserData> GetByIdAsync(Guid userId);
        Task<IEnumerable<UserData>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task DeleteAsync(Guid userId);
        Task<UserData> UpdateAsync(UserData toUpdate);
    }

    public class UserService : IUserService
    {
        readonly IBus _bus;
        readonly UsersContext _context;
        readonly IMapper _mapper;

        public UserService(IMapper mapper, UsersContext context, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<IEnumerable<UserData>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Users.Where(x => ids.Contains(x.UserId)).ToListAsync();
        }

        public async Task<UserData> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<UserData> CreateAsync(CreateUserDataRequest request)
        {
            var newHotel = _mapper.Map<UserData>(request);
            var result = await _context.Users.AddAsync(newHotel);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var toDelete = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            _context.Users.Remove(toDelete);
        }

        public async Task<UserData> UpdateAsync(UserData toUpdate)
        {
            var result = _context.Users.Update(toUpdate);
            var message = _mapper.Map<UserDataChangedMessage>(result);
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{QueuesUrls.Auth_UserChanged}"));
            endpoint.Send(message);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
