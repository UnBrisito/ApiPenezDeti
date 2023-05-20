using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class PohybCreateDto
    {
        [Required]
        public decimal Castka { get; set; }
        [Required]
        public string Detaily { get; set; }
    }
}
