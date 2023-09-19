using TiendaOrdenadoresWebApi.Models;
using Componente = TiendaOrdenadoresWebApi.Models.Componente;

namespace TiendaOrdenadoresWebApi.Services
{
    public class FakeRepositorioPedido : IRepositorioPedido
    {
        private readonly List<Ordenador> ListaOrdenadores1 = new();
        private readonly List<Componente> _listaComponentes1 = new();
        private readonly List<Pedido> _listaPedidos = new();

        public FakeRepositorioPedido()
        {

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
                TipoComponente = 1,
                Id = 3,
                OrdenadorId = 2
            });

            ListaOrdenadores1.Add(new Ordenador()
            {
                Id = 1,
                Descripcion = "Ordenador de Maria",
                Componentes = _listaComponentes1.FindAll(p => p.OrdenadorId == 1),
                PedidoId = 1
            });

            ListaOrdenadores1.Add(new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador de Andres",
                Componentes = _listaComponentes1.FindAll(p => p.OrdenadorId == 2),
                PedidoId = 2
            });

            _listaPedidos.Add(new Pedido() { Id = 1, Cliente = "Maria", NombrePedido = "Pedido de Maria", Ordenadores = ListaOrdenadores1.FindAll(p=>p.PedidoId == 1) });
            _listaPedidos.Add(new Pedido() { Id = 2, Cliente = "Andres", NombrePedido = "Pedido de Andres", Ordenadores = ListaOrdenadores1.FindAll(p=>p.PedidoId == 2) });

        }

        public void AddPedido(Pedido pedido)
        {
            var pedidoExiste = _listaPedidos.FirstOrDefault(c => c.Id == pedido.Id);
            if (pedidoExiste == null)
            {
                _listaPedidos.Add(pedido);
            }
        }

        public void BorraPedido(int id)
        {
            var pedidoABorrar = TomaPedido(id);
            if (pedidoABorrar != null)
            {
                _listaPedidos.Remove(pedidoABorrar);
            }
        }

        public Pedido TomaPedido(int id)
        {
            var pedidoEncontrado = _listaPedidos.FirstOrDefault(p => p.Id == id);
            if (pedidoEncontrado != null)
            {
                return pedidoEncontrado;
            }
            else
            {
                return new Pedido();
            }
        }

        public float DamePrecio(int id)
        {
            var pedido = _listaPedidos.Find(p => p.Id == id);
            float precio = 0;

            foreach (var ordenadores in pedido.Ordenadores)
            {
                foreach (var componentes in ordenadores.Componentes)
                {
                    precio += componentes.Coste;
                }
            }
            return precio;
        }

        public void UpdatePedido(Pedido pedido)
        {
            var PedidoEncontrado = TomaPedido(pedido.Id);
            var pedidoIndice = _listaPedidos.IndexOf(PedidoEncontrado);

            PedidoEncontrado.Ordenadores = pedido.Ordenadores;
            PedidoEncontrado.Id = pedido.Id;
            PedidoEncontrado.Cliente = pedido.Cliente;
            PedidoEncontrado.NombrePedido = pedido.NombrePedido;

            _listaPedidos[pedidoIndice] = PedidoEncontrado;
        }

        public List<Pedido> ListaPedido()
        {
            return _listaPedidos;
        }
    }
}
