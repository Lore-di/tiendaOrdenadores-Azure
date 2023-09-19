using TiendaA01.Models;
using Componente = TiendaA01.Models.Componente;

namespace TiendaA01.Services
{
    public class FakeRepositorioComponente : IRepositorioComponente
    {
        readonly List<Componente> _listaComponentes1 = new();

        readonly List<Pedido> _listaPedidos1 = new();

        private readonly List<Ordenador> _listaOrdenadores1 = new();

        //private readonly FakeRepositorioOrdenador repoOrd = new();

        public FakeRepositorioComponente()
        {
            var ordenador1 = new Ordenador() { Id = 2, Descripcion = "Ordenador Maria" };
            var ordenador2 = new Ordenador() { Id = 2, Descripcion = "Ordenador Andres" };

            // Procesador 1
            var procesador1 = new Componente()
            {
                Id = 3,
                Descripcion = "Procesador Intel i7",
                Calor = 10,
                Cores = 0,
                Coste = 9,
                Megas = 134,
                Serie = "789_XCS",
                TipoComponente = 0,
                OrdenadorId = 1,
                Ordenador = ordenador1
            };
            _listaComponentes1.Add(procesador1);

            // Disco Duro 1
            var discoDuro1 = new Componente()
            {
                Id = 2,
                Descripcion = "Disco Duro Scan Disk",
                Calor = 10,
                Cores = 500000,
                Coste = 50,
                Megas = 0,
                Serie = "789_XX",
                TipoComponente = 2,
                OrdenadorId = 1,
                Ordenador = ordenador1
            };
            _listaComponentes1.Add(discoDuro1);

            // Memoria 1
            var memoria1 = new Componente()
            {
                Id = 1,
                Descripcion = "Banco de memoria SDRAM",
                Calor = 10,
                Cores = 512,
                Coste = 100,
                Megas = 0,
                Serie = "879FH",
                TipoComponente = 1,
                OrdenadorId = 1,
                Ordenador = ordenador1
            };
            _listaComponentes1.Add(memoria1);

            // Procesador 2
            var procesador2 = new Componente()
            {
                Id = 4,
                Descripcion = "Procesador Ryzen",
                Calor = 60,
                Cores = 0,
                Coste = 34,
                Megas = 278,
                Serie = "797-X3",
                TipoComponente = 0,
                OrdenadorId = 2,
                Ordenador = ordenador2
            };
            _listaComponentes1.Add(procesador2);

            // Disco Duro 2
            var discoDuro2 = new Componente()
            {
                Id = 5,
                Descripcion = "Disco Duro Scan Disk",
                Calor = 39,
                Cores = 2000000,
                Coste = 128,
                Megas = 0,
                Serie = "789_XX_3",
                TipoComponente = 2,
                OrdenadorId = 2,
                Ordenador = ordenador2
            };
            _listaComponentes1.Add(discoDuro2);

            // Memorizador 2
            var memorizador2 = new Componente()
            {
                Id = 6,
                Descripcion = "Banco de memoria SDRAM",
                Calor = 24,
                Cores = 2028,
                Coste = 150,
                Megas = 0,
                Serie = "879FH_T",
                TipoComponente = 1,
                OrdenadorId = 2,
                Ordenador = ordenador2
            };
            _listaComponentes1.Add(memorizador2);
        }

        public void AddComponente(Componente componente)
        {
            _listaComponentes1.Add(componente);
        }

        public void BorraComponente(int id)
        {
            var componenteAborrar = TomaComponente(id);
            if(componenteAborrar != null)
                _listaComponentes1.Remove(componenteAborrar);
        }

        public List<Componente> ListaComponentes()
        {
            return _listaComponentes1;
        }

        public Componente TomaComponente(int id)
        {
            var componenteEncontrado = _listaComponentes1.FirstOrDefault(x => x.Id == id);
            if (componenteEncontrado != null)
            {
                return componenteEncontrado;
            }
            else
            {
                return new Componente();
            }
        }

        public void UpdateComponente(Componente componente)
        { 
            var componenteEncontrado = TomaComponente(componente.Id);
            var comp  = _listaComponentes1.IndexOf(componenteEncontrado);


                componenteEncontrado.Calor = componente.Calor;
                componenteEncontrado.Cores = componente.Cores;
                componenteEncontrado.Coste = componente.Coste;
                componenteEncontrado.Descripcion = componente.Descripcion;
                componenteEncontrado.Megas = componente.Megas;
                componenteEncontrado.Serie = componente.Serie;
                componenteEncontrado.TipoComponente = componente.TipoComponente;

            _listaComponentes1[comp] = componenteEncontrado;
        }
    }
}