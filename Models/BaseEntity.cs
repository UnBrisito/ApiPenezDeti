using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpravaPenezDeti.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CasVytoreni { get; set; }
    }
}
