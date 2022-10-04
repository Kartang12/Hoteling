using AutoMapper;
using ReviewApi.Contracts.Requests;
using ReviewApi.Domain;

namespace ReviewApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateReviewRequest, Review>();
        }
    }
}
