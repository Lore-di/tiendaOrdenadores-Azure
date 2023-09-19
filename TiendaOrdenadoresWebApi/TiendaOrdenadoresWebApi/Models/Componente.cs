using System.Text.Json.Serialization;

namespace TiendaOrdenadoresWebApi.Models
{
        public partial class Componente
        {
            public int Id { get; set; }
            public int Calor { get; set; } = 0;
            public string Descripcion { get; set; } = "0";
            public float Coste { get; set; } = 0;
            public long Megas { get; set; } = 0;
            public int Cores { get; set; } = 0;
            public string Serie { get; set; } = "";
            public int TipoComponente { get; set; } 
            // [JsonIgnore]
            public Ordenador? Ordenador { get; set; }
            public int OrdenadorId { get; set; }

        }
}
