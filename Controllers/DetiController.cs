//using Microsoft.AspNetCore.Components;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DetiController : ControllerBase
    {
        private readonly IRepo<Dite> _repository;
        private readonly IMapper _mapper;
        public DetiController(IRepo<Dite> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Dite>> Get()
        {
            return Ok(_repository.Get());
        }
        [HttpGet("{id}", Name = "GetDetiById")]
        public ActionResult<Dite> GetById(int id)
        {
            return Ok(_repository.GetById(id));
        }
        [HttpPost]
        public ActionResult<Dite> Create(DiteCreateDto diteCreate)
        {
            var dite = _mapper.Map<Dite>(diteCreate);
            _repository.Create(dite);
            _repository.SaveChanges();
            return CreatedAtRoute("GetDetiById", new { dite.Id }, dite);
        }
    }
}
