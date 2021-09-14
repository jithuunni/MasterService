using AutoMapper;
using MasterService.Api.Data.Entities;
using MasterService.Api.Models;
using MasterService.Api.Utilities;

namespace MasterService.Api.Mapper
{
    public class MasterServiceProfile : Profile
    {
        public MasterServiceProfile()
        {
            CreateMap<Country, CountryModel>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.id))
                .ForMember(dest => dest.CreatedDate, option => option.MapFrom(src => src._ts.ToDateTime()))
                .ReverseMap();
        }
    }
}
