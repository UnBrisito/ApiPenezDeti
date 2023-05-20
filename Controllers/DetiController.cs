//using Microsoft.AspNetCore.Components;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DetiController : GenericController<Dite, DiteCreateDto, DiteReadDto>
    {
        public DetiController (IRepo<Dite> repository, IMapper mapper) :base(repository, mapper) { }
        

    }
}
