using Microsoft.AspNetCore.Mvc;
using TiendaA01.Services;
using TiendaA01.ViewModels;

namespace TiendaA01.ViewComponents
{
    public class PedidosDetalladosViewComponent : ViewComponent
    {
        private readonly IRepositorioPedido _repositorioPedido;

        public PedidosDetalladosViewComponent(IRepositorioPedido repositorioPedido)
        {
            _repositorioPedido = repositorioPedido;
        }

        public IViewComponentResult Invoke(int pedidoId)
        {
            var pedidoDetallado = _repositorioPedido.TomaPedido(pedidoId);
            var precioPedido = _repositorioPedido.DamePrecio(pedidoId);
            var viewModel = new PedidoDetalladoViewModel
            {
                Pedido = pedidoDetallado,
                Precio = precioPedido
            };
            return View("Default", viewModel);
        }
    }
}
