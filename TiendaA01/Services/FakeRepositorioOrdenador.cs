using TiendaA01.Models;

    namespace TiendaA01.Services
    {
    public class FakeRepositorioOrdenador : IRepositorioOrdenador
    {
        private readonly List<Ordenador> ListaOrdenadores1 = new();
        private readonly List<Componente> _listaComponentes1 = new();

        public FakeRepositorioOrdenador()
        {

            var pedido1 = new Pedido() { Id = 1, Cliente = "Maria" };
            var pedido2 = new Pedido() { Id = 2, Cliente = "Andres" };

            var ordenador1 = new Ordenador() { Id = 2, Descripcion = "Ordenador Mariaaaaa" };
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
