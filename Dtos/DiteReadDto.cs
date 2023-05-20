using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class DiteReadDto
    {
        public int id { get; set; }
        public string CasVytoreni { get; set; }
        public string Jmeno { get; set; }
        public List<int> Ucty { get; set; }
    }
}
