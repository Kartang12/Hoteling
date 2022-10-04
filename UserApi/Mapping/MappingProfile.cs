using AutoMapper;
using UserApi.Contracts.Requests;
using UserApi.Domain;

namespace UserApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDataRequest, UserData>();
        }
    }
}
