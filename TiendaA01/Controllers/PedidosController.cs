
using Microsoft.AspNetCore.Mvc;
using TiendaA01.CrossCuting.Logging;
using TiendaA01.Models;
using TiendaA01.Services;

namespace TiendaA01.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IRepositorioPedido _repositorioPedido;
        private readonly IRepositorioOrdenador _repositorioOrdenador;
        private readonly ILoggerManager _logger;
        public PedidosController(IRepositorioPedido repositorio, IRepositorioOrdenador repositorioOrdenador, ILoggerManager loggerManager)
        {
            _repositorioPedido = repositorio;
            _repositorioOrdenador = repositorioOrdenador;
            _logger = loggerManager;
        }

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidosConOrdenadores = _repositorioPedido.ListaPedido();
              return View("Index", pedidosConOrdenadores);
        }

        // GET: Pedidos/ComponenteDetails/5
        public ActionResult Details(int id)
        {
            return View("Details", _repositorioPedido.ListaPedido().FirstOrDefault(p => p.Id == id));
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Cliente,NombrePedido")] Pedido pedido)
        {
            try
            {
                _repositorioPedido.AddPedido(pedido);
                _logger.LogInfo("Pedido creado");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogError("No se ha podido crear el pedido");
                return View(pedido);
            }
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            Pedido pedido = _repositorioPedido.TomaPedido(id);
            if (pedido == null)
            {
                _logger.LogError("Pedido no encontrado");
                return NotFound();
            }

            return View("Edit", pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido pedido)
        {
            try
            {
                _repositorioPedido.UpdatePedido(pedido);
                _logger.LogInfo("Pedido actualizado");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogError("Error al actualizar el pedido");
                return View("Edit");
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Delete", _repositorioPedido.TomaPedido(id));
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorioPedido.BorraPedido(id);
                _logger.LogInfo("Pedido eliminado");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogError("Error al eliminar el pedido");
                return BadRequest();
            }
        }
    }
}
