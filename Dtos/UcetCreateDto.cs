using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class UcetCreateDto
    {
        [Required]
        public string Jmeno { get; set; }
        public int? IdMajitele { get; set; }
    }
}
