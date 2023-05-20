using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/ucty/{UcetId}/Pohyby")]
    [ApiController]
    public class PohybyController : GenericController<Pohyb, PohybCreateDto, PohybReadDto>
    {
        private readonly IRepo<Ucet> _repoUcet;
        public PohybyController(IRepo<Pohyb> repository, IMapper mapper, IRepo<Ucet> repoDite) : base(repository, mapper)
        {
            _repoUcet = repoDite;
        }
        [HttpGet]
        public override ActionResult<List<PohybReadDto>> Get(int UcetId)
        {
            var entities = _repository.Get(p => p.UcetId==UcetId);
            if (entities == null) return NotFound();
            return Ok(_mapper.Map<IEnumerable<PohybReadDto>>(entities));
        }
        [HttpGet("{id}", Name = "GetById[Controller]")]
        public override ActionResult<PohybReadDto> GetById(int id, int UcetId)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound();
            if (entity.UcetId != UcetId)
            {
                return StatusCode(400, "Pohyb nepatří do účtu, kterého se žádost týká");
            }
            return Ok(_mapper.Map<PohybReadDto>(entity));
        }
        [HttpPost]
        public override ActionResult<PohybReadDto> Create(PohybCreateDto pohybCreate, int UcetId)
        {
            var entity = _mapper.Map<Pohyb>(pohybCreate);
            _repository.Create(entity);
            var ucet = _repoUcet.GetById(UcetId);
            entity.Ucet = ucet;

            ucet.Zustatek += entity.Castka;

            _repository.Update(entity);
            _repoUcet.Update(ucet);
            
            _repository.SaveChanges();
            var pohybRead = _mapper.Map<PohybReadDto>(entity);
            return CreatedAtRoute("GetById" + ControllerContext.ActionDescriptor.ControllerName, new { Id=pohybRead.Id, UcetId=ucet.Id }, pohybRead);
        }

    }
}
