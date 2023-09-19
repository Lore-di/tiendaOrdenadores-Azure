using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaA01.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Nombre del cliente mínimo 3 carácteres, máximo 40")]
        public string Cliente { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Nombre del pedido mínimo 3 carácteres, máximo 40")]
        public string NombrePedido { get; set; }
        public virtual ICollection<Ordenador> Ordenadores { get; set; }
    }
}
