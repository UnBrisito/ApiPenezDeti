using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpravaPenezDeti.Models
{
    public class Pohyb : BaseEntity
    {
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Castka { get; set; }
        [Required]
        public string Detaily { get; set; }
        [Required]
        public bool JeOdchozi { get; set; }
        [Required]
        public int UcetId { get; set; }
        [Required]
        public Ucet Ucet { get; set; }
    }
}
