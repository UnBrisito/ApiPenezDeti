using AutoMapper;

namespace SpravaPenezDeti.Profiles
{
    public class UctyProfiles : Profile
    {
        public UctyProfiles()
        {
            CreateMap<UcetCreateDto, Ucet>();
            CreateMap<Ucet, UcetReadDto>().ForMember(d => d.IdMajitele, options => options.MapFrom(src => src.Majitele.Select(a => a.Id)));
            CreateMap<Ucet, UcetUpdateDto>().ForMember(d => d.IdMajitele, options => options.MapFrom(src => src.Majitele.Select(a => a.Id)));
            CreateMap<UcetUpdateDto, Ucet>();
            
        }
    }
}
