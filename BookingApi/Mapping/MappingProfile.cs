using AutoMapper;
using BookingApi.Contracts.Requests;
using BookingApi.Contracts.Responses;
using BookingApi.Domain;
using HotelingLibrary.Messages;

namespace BookingApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingRequest>().ReverseMap();
            CreateMap<Booking, BookingResponse>();

            //var m = CreateMap<HotelDataChangedEvent, Booking>();
            //m.ForAllMembers(opt => opt.Ignore());
            //.ForMember(booking => booking.HotelName, src => src.MapFrom(hotel => hotel.Name));
            //CreateMap<HotelDataChangedMessage, Booking>()
            //    .ForMember(dest => dest.HotelName, opt =>
            //    {
            //        opt.PreCondition(src => !String.IsNullOrEmpty(src.Name));
            //        opt.MapFrom(src => src.Name);
            //    });

                //.ForMember(dest => dest.RoomNumber, opt => opt.Ignore())
                //.ForMember(dest => dest.RoomIdId, opt => opt.Ignore())
                //.ForMember(dest => dest.GuestsAmount, opt => opt.Ignore())
                //.ForMember(dest => dest.HotelId, opt => opt.Ignore())
                //.ForMember(dest => dest.StartDate, opt => opt.Ignore())
                //.ForMember(dest => dest.EdnDate, opt => opt.Ignore());

            //CreateMap<RoomDataChangedMessage, Booking>()
            //    .ForMember(dest => dest.RoomNumber, opts => opts.Condition((src, dest, srcV) => src.Number != null))
            //    .ForMember(dest => dest.RoomNumber, opt => {
            //         opt.PreCondition(src => (src.Number > 0));
            //         opt.MapFrom(src => src.Number);
            //     });
        }
    }
}
