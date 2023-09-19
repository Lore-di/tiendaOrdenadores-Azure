using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Componentes
{
    public class Componente : IComponente
    {
        private readonly ISerie _serie;
        private readonly IDescripcion _descripcion;
        private readonly ICalor _calor;
        private readonly IMegas _megas;
        private readonly ICores _cores;
        private readonly IPrecio _precio;
        private readonly EnumTipoComponente _enumTipoComponente;

        public Componente(ISerie serie, IDescripcion descripcion, ICalor calor, IMegas megas, ICores cores, IPrecio precio,
            EnumTipoComponente tipoComponente)
        {
            _serie = serie;
            _descripcion = descripcion;
            _calor = calor;
            _megas = megas;
            _cores = cores;
            _precio = precio;
            _enumTipoComponente = tipoComponente;
        }

        public decimal Coste { get => _precio.Coste; }
        public string NumeroSerie { get => _serie.NumeroSerie; }
        public int Calor { get => _calor.Calor; }
        public int Cores { get => _cores.Cores;  }
        public string Descripcion { get => _descripcion.Descripcion; }
        public long Megas { get => _megas.Megas;  }
        public EnumTipoComponente TipoComponente { get => _enumTipoComponente; }
    }
}
