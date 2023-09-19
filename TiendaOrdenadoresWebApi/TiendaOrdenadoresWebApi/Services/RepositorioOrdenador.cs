using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioOrdenador : IRepositorioOrdenador
    {
        private readonly TiendaA01Context _context;
        private readonly ILoggerManager _loggerManager;

        public RepositorioOrdenador(TiendaA01Context context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }

        public void AddOrdenador(Ordenador ordenador)
        {
            var ordenadorExiste = _context.Ordenador.FirstOrDefault(c => c.Id == ordenador.Id);
            if (ordenadorExiste == null)
            {
                _context.Add(ordenador);
                _loggerManager.LogInfo("Ordenador creado");
                _context.SaveChanges();
            }
        }

        public void BorraOrdenador(int id)
        {
            var ordenadorABorrar = TomaOrdenador(id);
            if (ordenadorABorrar != null)
            {
                _context.Ordenador.Remove(ordenadorABorrar);
                _loggerManager.LogInfo("Ordenador eliminado");
                _context.SaveChanges();
            }
        }

        public List<Ordenador> ListaOrdenadores()
        {
            return _context.Ordenador.Include(p => p.Componentes).Include(p => p.Pedido).AsNoTracking().ToList();
        }

        public Ordenador? TomaOrdenador(int id)
        {
            var ordenadorEncontrado = _context.Ordenador.AsNoTracking().Include(p => p.Componentes).Include(p => p.Pedido).FirstOrDefault(p => p.Id == id);
            if (ordenadorEncontrado != null)
            {
                _loggerManager.LogInfo("Ordenador encontrado");
                return ordenadorEncontrado;
            }
            else
            {
                _loggerManager.LogInfo("Ordenador no encontrado");
                return new Ordenador();
            }
        }

        public float DamePrecio(int id)
        {
            var precio = this._context.Componente.Where(p => p.OrdenadorId == id).Sum(p => p.Coste);
            return precio;
        }
        public void UpdateOrdenador(Ordenador ordenador)
        {
            _context.Update(ordenador);
            _loggerManager.LogInfo("Ordenador actualizado");
            _context.SaveChanges();
        }
    }
}
