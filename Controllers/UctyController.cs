using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UctyController : GenericController<Ucet, UcetCreateDto, UcetReadDto, UcetUpdateDto>
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
        [HttpPatch("{id}")]
        public override ActionResult patch(int id, JsonPatchDocument<UcetUpdateDto> patchDoc, int parentId = 0)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound();
            
            var entityUpdate = _mapper.Map<UcetUpdateDto>(entity);
            patchDoc.ApplyTo(entityUpdate, ModelState);

            foreach (var operation in patchDoc.Operations)
            {
                if (operation.OperationType == OperationType.Add)
                {
                    var dite = int.TryParse(operation.value.ToString(), out int a) ? _repoDite.GetById(a) : null;
                    entity.Majitele.Add(dite);
                }
            }

            if (!TryValidateModel(entityUpdate)) return ValidationProblem(ModelState);

            _mapper.Map(entityUpdate, entity);
            _repository.Update(entity);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
