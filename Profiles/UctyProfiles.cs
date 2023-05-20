using AutoMapper;

namespace SpravaPenezDeti.Profiles
{
    public class UctyProfiles : Profile
    {
        public UctyProfiles()
        {
            CreateMap<UcetCreateDto, Ucet>();
            CreateMap<Ucet, UcetReadDto>().ForMember(d => d.Majitele, options => options.MapFrom(src => src.Majitele.Select(a => a.Id)));
        }
    }
}
