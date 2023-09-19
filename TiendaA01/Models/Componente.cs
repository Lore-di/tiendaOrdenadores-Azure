
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaA01.Models
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

        public Ordenador Ordenador { get; set; }
        public int OrdenadorId { get; set; }

        private const float Tolerance = 0.01f;

         public override bool Equals(object? obj)
         {
             return Math.Abs(this.Coste - (obj as Componente)!.Coste) < Tolerance &&
                    this.TipoComponente == ((obj as Componente)!).TipoComponente && this.Megas == ((obj as Componente)!).Megas
                    && Math.Abs(this.Coste - ((obj as Componente)!).Coste) < Tolerance && this.Calor == ((obj as Componente)!).Calor &&
                    this.Descripcion == ((obj as Componente)!).Descripcion
                    && this.Serie == ((obj as Componente)!).Serie;
         }
    }
}
