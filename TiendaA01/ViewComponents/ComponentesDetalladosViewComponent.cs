using Microsoft.AspNetCore.Mvc;
using TiendaA01.Services;

namespace TiendaA01.ViewComponents
{
    public class ComponentesDetalladosViewComponent : ViewComponent
    {
        private readonly IRepositorioComponente _repositorioComponente;

        public ComponentesDetalladosViewComponent(IRepositorioComponente repositorioComponente)
        {
            _repositorioComponente = repositorioComponente;
        }

        public IViewComponentResult Invoke()
        {
            var componentesConDetalles = _repositorioComponente.ListaComponentes();
            return View("Default", componentesConDetalles);
        }
    }
}
