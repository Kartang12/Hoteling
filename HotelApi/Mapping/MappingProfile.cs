using AutoMapper;
using HotelApi.Contracts.Requests;
using HotelApi.Domain;
using HotelingLibrary.Messages;

namespace HotelApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateHotelRoomModel, Room>().
                ForMember(src => src.Hotel, x => x.Ignore());
            CreateMap<CreateHotelRequest, Hotel>().
                ForMember(src => src.Rooms, x => x.MapFrom(hotelRequest => hotelRequest.Rooms));
            CreateMap<CreateRoomRequest, Room>().
                ForMember(src => src.Hotel, x => x.Ignore());

            CreateMap<Hotel, HotelDataChangedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(hotel => hotel.Id));
            CreateMap<Hotel, HotelDeletedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(hotel => hotel.Id));
            CreateMap<Room, RoomDataChangedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(room => room.Id));
            CreateMap<Room, RoomDeletedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(room => room.Id));
        }
    }
}
