using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Models
{
    public class Dite : BaseEntity
    {
        
        [Required]
        [MaxLength(100)]
        public string Jmeno { get; set; }
        //[Required]
        public IEnumerable<Ucet>? Ucty { get; set; } = new List<Ucet>();
    }
}
