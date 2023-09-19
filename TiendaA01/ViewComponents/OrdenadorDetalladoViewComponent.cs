using Microsoft.AspNetCore.Mvc;
using TiendaA01.Services;
using TiendaA01.ViewModels;

namespace TiendaA01.ViewComponents
{
    public class OrdenadorDetalladoViewComponent : ViewComponent
    {
        private IRepositorioOrdenador _repositorioOrdenador;

        public OrdenadorDetalladoViewComponent(IRepositorioOrdenador repositorioOrdenador)
        {
            _repositorioOrdenador = repositorioOrdenador;
        }

        public IViewComponentResult Invoke(int ordenadorId)
        {
            var ordenadorDetallado = _repositorioOrdenador.TomaOrdenador(ordenadorId);
            var precioOrdenador = _repositorioOrdenador.DamePrecio(ordenadorId);

            var viewModel = new OrdenadorDetalladoViewModel
            {
                Ordenador = ordenadorDetallado,
                Precio = precioOrdenador
            };

            return View("default", viewModel);
        }
    }
}
