using AutoMapper;

namespace SpravaPenezDeti.Profiles
{
    public class DetiProfiles : Profile
    {
        public DetiProfiles()
        {
            CreateMap<DiteCreateDto, Dite>();
            CreateMap<Dite, DiteReadDto>().ForMember(d => d.Ucty, options => options.MapFrom(src => src.Ucty.Select(a=>a.Id)));
        }
    }
}
