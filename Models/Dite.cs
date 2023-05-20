using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpravaPenezDeti.Models
{
    public class Dite : BaseEntity
    {
        
        [Required]
        [MaxLength(100)]
        public string Jmeno { get; set; }
        //[Required]
        [ForeignKey("UcetIdd")]
        public List<Ucet> Ucty { get; set; } = new List<Ucet>();
    }
}
