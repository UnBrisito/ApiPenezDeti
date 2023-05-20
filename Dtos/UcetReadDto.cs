using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class UcetReadDto
    {
        public int id { get; set; }
        public string CasVytoreni { get; set; }
        public string Jmeno { get; set; }
        public List<int> Majitele { get; set; } = new List<int>();
        
        public decimal Zustatek { get; set; } = 0m;
    }
}
