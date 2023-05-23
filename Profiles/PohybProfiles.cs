using AutoMapper;

namespace SpravaPenezDeti.Profiles
{
    public class PohybProfiles : Profile
    {
        public PohybProfiles()
        {
            CreateMap<Pohyb, PohybReadDto>();
            CreateMap<PohybCreateDto, Pohyb>();
            CreateMap<Pohyb, PohybUpdateDto>();
            CreateMap<PohybUpdateDto, Pohyb>();
        }
    }
}
