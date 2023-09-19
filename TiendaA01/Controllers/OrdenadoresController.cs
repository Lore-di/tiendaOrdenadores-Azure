
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaA01.CrossCuting.Logging;
using TiendaA01.Data;
using TiendaA01.Models;
using TiendaA01.Services;

namespace TiendaA01.Controllers
{
    public class OrdenadoresController : Controller
    {
        private readonly IRepositorioOrdenador _repositorioOrdenador;
        private readonly IRepositorioPedido _repositorioPedido;
        private readonly IRepositorioComponente _repositorioComponente;
        private readonly ILoggerManager _loggerManager;

        public OrdenadoresController(IRepositorioPedido repositorio, IRepositorioOrdenador repositorioOrdenador,IRepositorioComponente repositorioComponente,
            ILoggerManager logger)
        {
            _repositorioPedido = repositorio;
            _loggerManager = logger;
            _repositorioOrdenador = repositorioOrdenador;
            _repositorioComponente = repositorioComponente;
        }

        // GET: Ordenadores
        public ActionResult Index()
        {
            var listaOrdenador = _repositorioOrdenador.ListaOrdenadores().ToList();
            return View("Index", listaOrdenador);
        }

        // GET: Ordenadores/ComponenteDetails/5
        public ActionResult Details(int id)
        {
            return View("Details", _repositorioOrdenador.ListaOrdenadores().FirstOrDefault(p => p.Id == id));
        }

        // GET: Ordenadores/Create
        public ActionResult Create()
        {
           ViewBag.ListaPedidos = ListaPedidos();
            return View("Create");
        }

        // POST: Ordenadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Descripcion,PedidoId")] Ordenador ordenador)
        {
            ViewBag.ListaPedidos = ListaPedidos();
            if (ordenador is not null) //validador
            {
                _repositorioOrdenador.AddOrdenador(ordenador);
                _loggerManager.LogInfo("El ordenador ha sido añadido");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ordenador);
            }
        }

        // GET: Ordenadores/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListaPedidos = ListaPedidos();
            
            Ordenador ordenador = _repositorioOrdenador.TomaOrdenador(id);

            if (ordenador == null)
            {
                _loggerManager.LogError("Ordenador no encontrado");
                return NotFound();
            }

            return View("Edit", ordenador);
        }

        // POST: Ordenadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ordenador ordenador)
        {
            ViewBag.ListaPedidos = ListaPedidos();
            try
            {
                _repositorioOrdenador.UpdateOrdenador(ordenador);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _loggerManager.LogError("El ordenador no se ha podido editar");
                return View();
            }
        }

        // GET: Ordenadores/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Delete", _repositorioOrdenador.TomaOrdenador(id));
        }

        // POST: Ordenadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorioOrdenador.BorraOrdenador(id);
                _loggerManager.LogInfo("Ordenador eliminado");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _loggerManager.LogError("Error al eliminar el ordenador");
                return BadRequest();
            }
        }

        public List<SelectListItem> ListaComponentes()
        {
            var componentes = _repositorioComponente.ListaComponentes();

            var selectListComponentes = componentes.Select(p=> new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Descripcion
            }).ToList();
            return selectListComponentes;
        }

        public List<SelectListItem> ListaPedidos()
        {
            var pedidos = _repositorioPedido.ListaPedido();
            var selectListPedidos = pedidos.Select(p=> new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.NombrePedido
            }).ToList();
            return selectListPedidos;
        }
    }
}