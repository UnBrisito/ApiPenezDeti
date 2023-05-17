using AutoMapper;
namespace SpravaPenezDeti.Profiles
{
    public class DetiProfiles : Profile
    {
        public DetiProfiles()
        {
            CreateMap<DiteCreateDto, Dite>();
        }

    }
}
