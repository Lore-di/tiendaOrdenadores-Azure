
using TiendaOrdenadoresWebApi.Models;
using Componente = TiendaOrdenadoresWebApi.Models.Componente;

namespace TiendaOrdenadoresWebApi.Services
{
    public class FakeRepositorioOrdenador : IRepositorioOrdenador
    {
        private readonly List<Ordenador> ListaOrdenadores1 = new();
        private readonly List<Componente> _listaComponentes1 = new();

        public FakeRepositorioOrdenador()
        {

            var pedido1 = new Pedido() { Id = 1, Cliente = "Maria", NombrePedido = "Pedido prueba maria" };
            var pedido2 = new Pedido() { Id = 2, Cliente = "Andres", NombrePedido = "Pedido prueba Andres" };

            _listaComponentes1.Add(new Componente()
            {
                Calor = 10,
                Cores = 0,
                Coste = 9.0f,
                Descripcion = "Procesador Intel i7",
                Megas = 134,
                Serie = "789_XCS",
                TipoComponente = 0,
                Id = 3,
                OrdenadorId = 1
            });

            _listaComponentes1.Add(new Componente()
            {
                Calor = 10,
                Cores = 0,
                Coste = 50.0f,
                Descripcion = "Disco Duro Scan Disk",
                Megas = 500000,
                Serie = "789_XX",
                TipoComponente = 2,
                Id = 2,
                OrdenadorId = 1
            });

            _listaComponentes1.Add(new Componente()
            {
                Calor = 10,
                Cores = 512,
                Coste = 100.0f,
                Descripcion = "Banco de memoria SDRAM",
                Megas = 0,
                Serie = "879FH",
                TipoComponente = 1,
                Id = 1,
                OrdenadorId = 1
            });

            _listaComponentes1.Add(new Componente()
            {
                Calor = 60,
                Cores = 34,
                Coste = 278.0f,
                Descripcion = "Procesador Ryzen",
                Megas = 0,
                Serie = "797-X3",
                TipoComponente = 0,
                Id = 3,
                OrdenadorId = 2
            });

            _listaComponentes1.Add(new Componente()
            {
                Calor = 39,
                Cores = 0,
                Coste = 128.0f,
                Descripcion = "Disco Duro Scan Disk",
                Megas = 2000000,
                Serie = "789_XX_3",
                TipoComponente = 3,
                Id = 2,
                OrdenadorId = 2
            });

            _listaComponentes1.Add(new Componente()
            {
                Calor = 24,
                Cores = 2028,
                Coste = 150.0f,
                Descripcion = "Banco de memoria SDRAM",
                Megas = 0,
                Serie = "879FH_T",
                TipoComponente =1,
                Id = 3,
                OrdenadorId = 2
            });


            ListaOrdenadores1.Add(new Ordenador()
            {
                Id = 1,
                Descripcion = "Ordenador de Maria",
                Componentes = _listaComponentes1.FindAll(p => p.OrdenadorId == 1),
                Pedido = pedido1
            });

            ListaOrdenadores1.Add(new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador de Andres",
                Componentes = _listaComponentes1.FindAll(p => p.OrdenadorId == 2),
                Pedido = pedido2
            });
        }

        public void AddOrdenador(Ordenador ordenador)
        {
            var ordenadorExiste = ListaOrdenadores1.FirstOrDefault(c => c.Id == ordenador.Id);
            if (ordenadorExiste == null)
            {
                ListaOrdenadores1.Add(ordenador);
            }
        }

        public void BorraOrdenador(int id)
        {
            var ordenadorABorrar = TomaOrdenador(id);
            if (ordenadorABorrar != null)
            {
                ListaOrdenadores1.Remove(ordenadorABorrar);
            }
        }

        public List<Ordenador> ListaOrdenadores()
        {
            return ListaOrdenadores1;
        }

        public Ordenador? TomaOrdenador(int id)
        {
            var ordenadorEncontrado = ListaOrdenadores1.FirstOrDefault(p => p.Id == id);
            if (ordenadorEncontrado != null)
            {
                return ordenadorEncontrado;
            }
            else
            {
                return new Ordenador();
            }
        }

        public float DamePrecio(int id)
        {
            var ordenador = ListaOrdenadores1.Find(p => p.Id == id);
            float precio = 0;

            foreach (var componentes in ordenador.Componentes)
            {
                precio += componentes.Coste;
            }
            return precio;
        }

        public void UpdateOrdenador(Ordenador ordenador)
        {
            var OrdenadorEncontrado = TomaOrdenador(ordenador.Id);
            var ord = ListaOrdenadores1.IndexOf(OrdenadorEncontrado);

            OrdenadorEncontrado.Pedido = ordenador.Pedido;
            OrdenadorEncontrado.Componentes = ordenador.Componentes;
            OrdenadorEncontrado.Id = ordenador.Id;
            OrdenadorEncontrado.Descripcion = ordenador.Descripcion;
            OrdenadorEncontrado.PedidoId = ordenador.PedidoId;

            ListaOrdenadores1[ord] = OrdenadorEncontrado;
        }
    }
}
