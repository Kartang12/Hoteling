using AutoMapper;
using HotelingLibrary.Messages;
using ReviewApi.Contracts.Requests;
using ReviewApi.Domain;

namespace ReviewApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateReviewRequest, Review>();
            CreateMap<Review, ReviewAddedMessage>()
                .ForMember(x => x.EntityId, x => x.MapFrom(review => review.UserId));
        }
    }
}
