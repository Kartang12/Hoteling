using AutoMapper;
using HotelingLibrary.Messages;
using UserApi.Contracts.Requests;
using UserApi.Domain;

namespace UserApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDataRequest, UserData>();
            CreateMap<UserData, UserDataChangedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(user => user.UserId));
            CreateMap<UserData, UserDeletedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(user => user.UserId));
        }
    }
}
