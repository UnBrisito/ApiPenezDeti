using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UcetController
    {
        private readonly IRepo<Ucet> _repository;
        private readonly IMapper _mapper;
        public UcetController(IRepo<Ucet> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
