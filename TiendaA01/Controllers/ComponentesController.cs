using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using TiendaA01.CrossCuting.Logging;
using TiendaA01.Models;
using TiendaA01.Services;

namespace TiendaA01.Controllers
{
    public class ComponentesController : Controller
    {
        private readonly IRepositorioComponente _repositorioComponente;
        private readonly IRepositorioOrdenador _repositorioOrdenador;
        private readonly ILoggerManager _loggerManager;

        public ComponentesController(IRepositorioComponente repositorio, ILoggerManager loggerManager, IRepositorioOrdenador repositorioOrdenador)
        {
            _repositorioOrdenador = repositorioOrdenador;
            _repositorioComponente = repositorio;
            _loggerManager = loggerManager;
        }

        // GET: Componentes
        public ActionResult Index()
        {
            _loggerManager.LogInfo("Se va a mostrar la lista de componentes");
            return View("Index", _repositorioComponente.ListaComponentes());
        }

        // GET: Componentes/ComponenteDetails/5
        public ActionResult ComponenteDetails(int id)
        {
            return View("ComponenteDetails", _repositorioComponente.TomaComponente(id));
        }

        // GET: Componentes/Create
        public ActionResult Create()
        {
            ViewBag.ListaOrdenadores = ListaOrdenadores();
            return View("Create");
        }

        // POST: Componentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Calor,Descripcion,Coste,Megas,Cores,Serie,TipoComponente, OrdenadorId")] Componente componente)
        {
            ViewBag.ListaOrdenadores = ListaOrdenadores();
            try
            {
                _repositorioComponente.AddComponente(componente);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View(componente);
            }

        }

        // GET: Componentes/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListaOrdenadores = ListaOrdenadores();
           // ViewData["GetEnumComponenteList"] = GetEnumComponenteList();
            Componente componente = _repositorioComponente.TomaComponente(id);
            if (componente == null)
            {
                return NotFound();
            }
            return View("Edit", componente);
        }

        /*public List<SelectListItem> GetEnumComponenteList()
        {
            return Enum.GetValues(typeof(EnumComponente))
                .Cast<EnumComponente>()
                .Select(p => new SelectListItem
                {
                    Value = ((int)p).ToString(),
                    Text = p.ToString()
                })
                .ToList();
        }*/

        public List<SelectListItem> ListaOrdenadores()
        {
            var ordenadoresLista = _repositorioOrdenador.ListaOrdenadores();
            var ordenadores= ordenadoresLista.Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Descripcion
                })
                .ToList();
            return ordenadores;
        }

        // POST: Componentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Calor,Descripcion,Coste,Megas,Cores,Serie,TipoComponente, OrdenadorId")] Componente componente)
        {
            ViewBag.ListaOrdenadores = ListaOrdenadores();
            try
            {
                _repositorioComponente.UpdateComponente(componente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Componentes/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Delete", _repositorioComponente.TomaComponente(id));
        }

        // POST: Componentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repositorioComponente.BorraComponente(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult componentesDetalles()
        {
            return View("ComponentesConDetalles");
        }

    }
}
