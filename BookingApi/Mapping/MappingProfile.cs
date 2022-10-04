using AutoMapper;
using BookingApi.Contracts.Requests;
using BookingApi.Domain;

namespace BookingApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingRequest>().ReverseMap();
            
        }
    }
}
