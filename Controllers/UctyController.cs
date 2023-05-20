using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UctyController : GenericController<Ucet, UcetCreateDto, UcetReadDto>
    {
        private readonly IRepo<Dite> _repoDite;
        public UctyController(IRepo<Ucet> repository, IMapper mapper, IRepo<Dite> repoDite) : base(repository, mapper)
        {
            _repoDite = repoDite;
        }
        [HttpPost]
        public override ActionResult<UcetReadDto> Create(UcetCreateDto ucetCreate, int parentId = 0)
        {
            var entity = _mapper.Map<Ucet>(ucetCreate);
            _repository.Create(entity);
            if (ucetCreate.IdMajitele is int a)
            {
                var dite = _repoDite.GetById(a);
                entity.Majitele.Add(dite);
            }
            _repository.Update(entity);
            _repository.SaveChanges();

            var ucetRead = _mapper.Map<UcetReadDto>(_repository.GetById(entity.Id));
            return CreatedAtRoute("GetById" + ControllerContext.ActionDescriptor.ControllerName, new { ucetRead.id }, ucetRead);
        }
       
    }
}
