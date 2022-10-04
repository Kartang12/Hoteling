using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.Domain;

namespace HotelApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateHotelRequest, Hotel>();
            CreateMap<CreateRoomRequest, Room>();
        }
    }
}
