using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpravaPenezDeti.Models
{
    public class Ucet : BaseEntity
    {
        [Required]
        public string Jmeno { get; set; }
        [ForeignKey("DiteIdd")]
        public List<Dite> Majitele { get; set; } = new List<Dite>();
        public List<Pohyb> Pohyby { get; set; } = new List<Pohyb>();
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Zustatek { get; set; } = 0m;
    }
}
