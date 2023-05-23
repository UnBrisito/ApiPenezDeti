using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class UcetUpdateDto
    {
        [Required]
        public string Jmeno { get; set; }
        public List<int> IdMajitele { get; set; }
    }
}
