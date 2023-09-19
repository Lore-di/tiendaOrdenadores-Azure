using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TiendaA01.Models
{
    public class ComponenteAnnotation
    {
        public int Id { get; set; }

        [Required] [Range(0, int.MaxValue)] public int Calor { get; set; } = 0;

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Descripcion mínimo 3 carácteres")]
        [DataType(DataType.MultilineText)]
        [DefaultValue("Sustituya este texto por la descripción del componente")]
        public string Descripcion { get; set; } = "";

        [Required]
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal Coste { get; set; } = 0;

        [Required] [Range(0, 10)] public long Megas { get; set; } = 0;
        [Required] [Range(0, 10)] public int Cores { get; set; } = 0;

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Serie mínimo 3 carácteres")]
        [DefaultValue("Escriba el número de serie del componente")]
        public string Serie { get; set; } = "";
        
        [Required]
        public int TipoComponente { get; set; }

    }
    [MetadataType(typeof(ComponenteAnnotation))]
    public partial class Componente
    {
    }
}
