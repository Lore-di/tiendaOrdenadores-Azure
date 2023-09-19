using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TiendaOrdenadoresWebApi.Models
{
    public class Ordenador
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Descripcion mínimo 3 carácteres")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; } = "";
        public virtual ICollection<Componente> Componentes { get; set; }
        public int PedidoId { get; set; }
        [JsonIgnore]
        public Pedido Pedido { get; set; }
    }
}
