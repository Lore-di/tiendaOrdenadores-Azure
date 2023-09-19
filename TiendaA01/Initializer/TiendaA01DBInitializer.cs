/*using Microsoft.EntityFrameworkCore;
using TiendaA01.Data;
using TiendaA01.Models;
using Componente = TiendaA01.Models.Componente;

namespace TiendaA01.Initializer
{
    public class TiendaA01DbInitializer
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using var context =
                new TiendaA01Context(serviceProvider.GetRequiredService<DbContextOptions<TiendaA01Context>>());
            if (context.Componente.Any())
            {
                return;
            }

            List<Pedido> listaPedidos = new List<Pedido>();

            listaPedidos.Add(new Pedido(){ Cliente = "Maria", NombrePedido = "Pedido de Maria"});
            listaPedidos.Add(new Pedido(){ Cliente = "Andres", NombrePedido = "Pedido de Andres"});
            context.Pedido.AddRange(listaPedidos);
            context.SaveChanges();

            List<Ordenador> listaOrdenadores = new List<Ordenador>();

            listaOrdenadores.Add(new Ordenador(){Descripcion = "Ordenador Maria", PedidoId = context.Pedido.Single(p=>p.Cliente == "Maria").Id});
            listaOrdenadores.Add(new Ordenador(){Descripcion = "Ordenador de Andres", PedidoId = context.Pedido.Single(p=>p.Cliente == "Andres").Id});
            context.Ordenador.AddRange(listaOrdenadores);
            context.SaveChanges();
            
            List<Componente> listaComponentes = new List<Componente>();
            var builderComponente = new BuilderComponente();

            var componente1 = builderComponente.DameComponente(EnumComponente.ProcesadorInteli7789Xcd);
            var componente2 = builderComponente.DameComponente(EnumComponente.DiscoDuroSanDisk789Xx);
            var componente3 = builderComponente.DameComponente(EnumComponente.BancoDeMemoriaSdram879FhL);
            var componente4 = builderComponente.DameComponente(EnumComponente.DiscoDuroSanDisk789Xx2);
            var componente5 = builderComponente.DameComponente(EnumComponente.ProcesadorInteli7789Xcs);
            var componente6 = builderComponente.DameComponente(EnumComponente.BancoDeMemoriaSdram879FhT);
            var componente7 = builderComponente.DameComponente(EnumComponente.ProcesadorInteli7789Xct);
            var componente8 = builderComponente.DameComponente(EnumComponente.DiscoDuroSanDisk789Xx3);
            var componente9 = builderComponente.DameComponente(EnumComponente.BancoDeMemoriaSdram879Fh);
            var componente10 = builderComponente.DameComponente(EnumComponente.ProcesadorRyzenAMD);

            //if(componente1 != null)
                //listaComponentes.Add(new Componente() {Calor = componente1.Calor, Cores = componente1.Cores, Coste = (float)componente1.Coste, Descripcion = componente1.Descripcion, Megas = componente1.Megas, Serie = componente1.NumeroSerie, TipoComponente = (int)componente1.TipoComponente, OrdenadorId = 1});
            if(componente2 != null)
                listaComponentes.Add(new Componente() { Calor = componente2.Calor, Cores = componente2.Cores, Coste = (float)componente2.Coste, Descripcion = componente2.Descripcion, Megas = componente2.Megas, Serie = componente2.NumeroSerie, TipoComponente = (int)componente2.TipoComponente, OrdenadorId = context.Ordenador.Single(p=>p.Descripcion == "Ordenador Maria").Id });
            //if(componente3 != null)
               // listaComponentes.Add(new Componente() { Calor = componente3.Calor, Cores = componente3.Cores, Coste = (float)componente3.Coste, Descripcion = componente3.Descripcion, Megas = componente3.Megas, Serie = componente3.NumeroSerie, TipoComponente = (int)componente3.TipoComponente, OrdenadorId = 1});
            //if(componente4 != null)
                //listaComponentes.Add(new Componente() { Calor = componente4.Calor, Cores = componente4.Cores, Coste = (float)componente4.Coste, Descripcion = componente4.Descripcion, Megas = componente4.Megas, Serie = componente4.NumeroSerie, TipoComponente = (int)componente4.TipoComponente , OrdenadorId = 2 });
            if(componente5 != null)
                listaComponentes.Add(new Componente() { Calor = componente5.Calor, Cores = componente5.Cores, Coste = (float)componente5.Coste, Descripcion = componente5.Descripcion, Megas = componente5.Megas, Serie = componente5.NumeroSerie, TipoComponente = (int)componente5.TipoComponente, OrdenadorId = context.Ordenador.Single(p=>p.Descripcion == "Ordenador Maria").Id });
            if(componente6 != null)
                listaComponentes.Add(new Componente() { Calor = componente6.Calor, Cores = componente6.Cores, Coste = (float)componente6.Coste, Descripcion = componente6.Descripcion, Megas = componente6.Megas, Serie = componente6.NumeroSerie, TipoComponente = (int)componente6.TipoComponente, OrdenadorId = context.Ordenador.Single(p=>p.Descripcion == "Ordenador de Andres").Id });
            //if(componente7 != null)
                //listaComponentes.Add(new Componente() { Calor = componente7.Calor, Cores = componente7.Cores, Coste = (float)componente7.Coste, Descripcion = componente7.Descripcion, Megas = componente7.Megas, Serie = componente7.NumeroSerie, TipoComponente = (int)componente7.TipoComponente });
            if(componente8 != null)
                listaComponentes.Add(new Componente() { Calor = componente8.Calor, Cores = componente8.Cores, Coste = (float)componente8.Coste, Descripcion = componente8.Descripcion, Megas = componente8.Megas, Serie = componente8.NumeroSerie, TipoComponente = (int)componente8.TipoComponente, OrdenadorId = context.Ordenador.Single(p=>p.Descripcion == "Ordenador de Andres").Id });
            if(componente9 != null)
                listaComponentes.Add(new Componente() { Calor = componente9.Calor, Cores = componente9.Cores, Coste = (float)componente9.Coste, Descripcion = componente9.Descripcion, Megas = componente9.Megas, Serie = componente9.NumeroSerie, TipoComponente = (int)componente9.TipoComponente , OrdenadorId = context.Ordenador.Single(p => p.Descripcion == "Ordenador Maria").Id });
            if (componente9 != null)
                listaComponentes.Add(new Componente() { Calor = componente10.Calor, Cores = componente10.Cores, Coste = (float)componente10.Coste, Descripcion = componente10.Descripcion, Megas = componente10.Megas, Serie = componente10.NumeroSerie, TipoComponente = (int)componente10.TipoComponente, OrdenadorId = context.Ordenador.Single(p => p.Descripcion == "Ordenador de Andres").Id });
            context.Componente.AddRange(listaComponentes);

            context.SaveChanges();
        }
    }
}*/
