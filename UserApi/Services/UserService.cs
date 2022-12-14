using AutoMapper;
using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
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
        Task ConsumeReviewDeletedMessage(ConsumeContext<ReviewDeletedMessage> consumeContext);
    }

    public class UserService : IUserService
    {
        readonly IPublishEndpoint _endpoint;
        readonly UsersContext _context;
        readonly IMapper _mapper;

        public UserService(IMapper mapper, UsersContext context, IPublishEndpoint endpoint)
        {
            _context = context;
            _mapper = mapper;
            _endpoint = endpoint;
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
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var toDelete = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            _context.Users.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<UserData> UpdateAsync(UserData toUpdate)
        {
            var result = _context.Users.Update(toUpdate);
            var message = _mapper.Map<UserDataChangedMessage>(result);
            await _endpoint.Publish(message);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task ConsumeReviewDeletedMessage(ConsumeContext<ReviewDeletedMessage> consumeContext)
        {
            List<UserData> updatedUsers = new List<UserData>();
            consumeContext.Message.UsersDeletedReviews.ForEach(
                x =>
                {
                    var user = _context.Users.First(u => u.UserId == x);
                    user.ReviewsAmount--;
                    updatedUsers.Add(user);
                });
            _context.Users.UpdateRange(updatedUsers);
            await _context.SaveChangesAsync();
        }
    }
}
