using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services
{
    public class RepositorioPedido : IRepositorioPedido
    {
        private readonly TiendaA01Context _context;
        private readonly ILoggerManager _logger;

        public RepositorioPedido(TiendaA01Context dbContext, ILoggerManager logger)
        {
            this._context = dbContext;
            this._logger = logger;
        }

        public void AddPedido(Pedido pedido)
        {
            var existePedido = _context.Pedido.FirstOrDefault(c => c.Id == pedido.Id);
            if (existePedido == null)
            {
                _context.Add(pedido);
                _logger.LogInfo("Pedido añadido");
                _context.SaveChanges();
            }
        }

        public void BorraPedido(int id)
        {
            var componenteABorrar = TomaPedido(id);
            _context.Remove(componenteABorrar);
            _logger.LogInfo("Pedido borrado");
            _context.SaveChanges();
        }

        public List<Pedido> ListaPedido()
        {
            return _context.Pedido.Include(p => p.Ordenadores).ThenInclude(p => p.Componentes).AsNoTracking().ToList();
        }

        public Pedido? TomaPedido(int id)
        {
            var pedidoEncontrado = _context.Pedido.AsNoTracking().Include(p => p.Ordenadores).ThenInclude(p => p.Componentes).FirstOrDefault(p => p.Id == id);

            if (pedidoEncontrado != null)
            {
                _logger.LogInfo("Pedido encontrado");
                return pedidoEncontrado;
            }
            else
            {
                _logger.LogInfo("Pedido no encontrado");
                return new Pedido();
            }
        }

        public float DamePrecio(int id)
        {
            var precioTotal = _context.Ordenador
                .Where(p => p.PedidoId == id)
                .SelectMany(p => p.Componentes)
                .Sum(c => c.Coste);

            return precioTotal;
        }
        public void UpdatePedido(Pedido pedido)
        {
            // pedido.Precio = damePrecio(pedido.Id);
            _context.Update(pedido);
            _logger.LogInfo("Pedido actualizado");
            _context.SaveChanges();
        }
    }
}
