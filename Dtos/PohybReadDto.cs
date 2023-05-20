using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class PohybReadDto
    {
        public int Id { get; set; }
        public string CasVytoreni { get; set; }
        public decimal Castka { get; set; }
        public string Detaily { get; set; }
        public int UcetId { get; set; }
    }
}
