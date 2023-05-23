using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SpravaPenezDeti.Controllers
{
    [Route("api/ucty/{UcetId}/Pohyby")]
    [ApiController]
    public class PohybyController : GenericController<Pohyb, PohybCreateDto, PohybReadDto, PohybUpdateDto>
    {
        private readonly IRepo<Ucet> _repoUcet;
        public PohybyController(IRepo<Pohyb> repository, IMapper mapper, IRepo<Ucet> repoDite) : base(repository, mapper)
        {
            _repoUcet = repoDite;
        }
        [HttpGet]
        public override ActionResult<List<PohybReadDto>> Get(int UcetId)
        {
            var queryParameters = HttpContext.Request.Query;
            List<Func<Pohyb, bool>> filtry = getFilters(queryParameters, out int stranka);
            filtry.Add(p => p.UcetId == UcetId);
            
            var entities = _repository.Get(stranka, filtry.ToArray());
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
            return CreatedAtRoute("GetById" + ControllerContext.ActionDescriptor.ControllerName, new { Id = pohybRead.Id, UcetId = ucet.Id }, pohybRead);
        }
        [HttpDelete("{id}")]
        public override ActionResult Delete(int id, int UcetId)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return NotFound();
            _repository.Delete(entity);
            var ucet = _repoUcet.GetById(UcetId);
            ucet.Zustatek -= entity.Castka;
            _repository.SaveChanges();
            return NoContent();
        }



        //Tohle je hrozný
        private List<Func<Pohyb, bool>> getFilters(IQueryCollection queryParameters, out int stranka)
        {
            List<Func<Pohyb, bool>> filtry = new List<Func<Pohyb, bool>>();
            stranka = 0;
            foreach (var parametr in queryParameters)
            {
                var strs = Regex.Split(parametr.Key.Trim().ToLower(), @"(max|min)");
                Console.WriteLine(strs[strs.Count() - 1]);
                Console.WriteLine(parametr.Value);
                if (typeof(PohybReadDto).GetProperty(strs[strs.Count() - 1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    var a = typeof(Pohyb).GetProperty(strs[strs.Count() - 1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (a.PropertyType == typeof(string))
                    {
                        Console.WriteLine(222);
                        filtry.Add(p => a.GetValue(p).ToString().Contains(parametr.Value));
                    }
                    else
                    {
                        string comparator = strs.Count() > 1 ? strs[strs.Count() - 2] : "equal";
                        Func<Pohyb, bool> f;
                        switch (a.PropertyType)
                        {
                            case Type intType when intType == typeof(int) && comparator=="equal": 
                                f = (p => (int)a.GetValue(p) == int.Parse(parametr.Value));
                                break;
                            case Type intType when intType == typeof(int) && comparator == "max":
                                f = (p => (int)a.GetValue(p) <= int.Parse(parametr.Value));
                                break;
                            case Type intType when intType == typeof(int) && comparator == "min":
                                f = (p => (int)a.GetValue(p) >= int.Parse(parametr.Value));
                                break;
                            case Type decimalType when decimalType == typeof(decimal) && comparator == "equal":
                                f = (p => (decimal)a.GetValue(p) == decimal.Parse(parametr.Value));
                                break;
                            case Type decimalType when decimalType == typeof(decimal) && comparator == "max":
                                f = (p => (decimal)a.GetValue(p) <= decimal.Parse(parametr.Value));
                                break;
                            case Type decimalType when decimalType == typeof(decimal) && comparator == "min":
                                f = (p => (decimal)a.GetValue(p) >= decimal.Parse(parametr.Value));
                                break;
                            case Type DateTimeType when DateTimeType == typeof(DateTime) && comparator == "equal":
                                f = (p => ((DateTime)a.GetValue(p)).Date == DateTime.Parse(parametr.Value).Date);
                                break;
                            case Type DateTimeType when DateTimeType == typeof(DateTime) && comparator == "max":
                                f = (p => (DateTime)a.GetValue(p) <= DateTime.Parse(parametr.Value));
                                break;
                            case Type DateTimeType when DateTimeType == typeof(DateTime) && comparator == "min":
                                f = (p => (DateTime)a.GetValue(p) >= DateTime.Parse(parametr.Value));
                                break;
                            default:
                                f = null; 
                                break;

                        }
                        
                        if(f != null) filtry.Add(f);
                    }
                }
                else
                {
                    if (parametr.Key.ToLower() == "stranka")
                    {
                        stranka = int.Parse(parametr.Value)-1;
                    }
                }

            }
            return filtry;
        }
        private int compare<V>(V a, V b) where V : struct, IComparable<V>
        {
            return a.CompareTo(b);
        }
    }
}
