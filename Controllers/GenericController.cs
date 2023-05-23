using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace SpravaPenezDeti.Controllers
{
    public abstract class GenericController<T, TCreateDto, TReadDto, TUpdateDto> : ControllerBase where T : BaseEntity where TUpdateDto : class
    {
        protected readonly IRepo<T> _repository;
        protected readonly IMapper _mapper;
        public GenericController(IRepo<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public virtual ActionResult<List<TReadDto>> Get(int parentId = 0)
        {
            var entities = _repository.Get();
            if (entities == null) return NotFound();
            return Ok(_mapper.Map<IEnumerable<TReadDto>>(entities));
        }
        [HttpGet("{id}", Name = "GetById[Controller]")]
        public virtual ActionResult<TReadDto> GetById(int id, int parentId = 0)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound(); 
            return Ok(_mapper.Map<TReadDto>(entity));
        }
        [HttpPost]
        public virtual ActionResult<TReadDto> Create(TCreateDto createDto, int parentId = 0)
        {
            var entity = _mapper.Map<T>(createDto);
            _repository.Create(entity);
            _repository.SaveChanges();
            return CreatedAtRoute("GetById" + ControllerContext.ActionDescriptor.ControllerName, new { entity.Id }, entity);
        }
        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int id, int parentId = 0)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound();
            _repository.Delete(entity);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public virtual ActionResult patch(int id, JsonPatchDocument<TUpdateDto> patchDoc, int parentId = 0)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound();

            var entityUpdate = _mapper.Map<TUpdateDto>(entity);
            patchDoc.ApplyTo(entityUpdate, ModelState);

            if (!TryValidateModel(entityUpdate)) return ValidationProblem(ModelState);

            _mapper.Map(entityUpdate, entity);
            _repository.Update(entity);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
