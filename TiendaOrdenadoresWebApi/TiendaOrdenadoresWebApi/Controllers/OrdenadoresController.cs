using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaOrdenadoresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenadoresController : ControllerBase
    {
        private readonly IRepositorioOrdenador _repositorioOrdenador;

        public OrdenadoresController(IRepositorioOrdenador repositorioOrdenador)
        {
            _repositorioOrdenador = repositorioOrdenador;
        }
        // GET: api/<OrdenadoresController>
        [HttpGet]
        public ActionResult Get()
        {
            var ordenadores = _repositorioOrdenador.ListaOrdenadores();
            if (ordenadores == null)
                return NotFound();
            return Ok(ordenadores);
        }

        // GET api/<OrdenadoresController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var ordenador = _repositorioOrdenador.TomaOrdenador(id);
            if(ordenador == null)
                return NotFound();
            return Ok(ordenador);
        }

        // POST api/<OrdenadoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Ordenador ordenador)
        {
            if(ordenador == null)
                return BadRequest();

            _repositorioOrdenador.AddOrdenador(ordenador);
            return CreatedAtAction(nameof(Get), new { id = ordenador.Id }, ordenador);
        }

        // PUT api/<OrdenadoresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ordenador ordenador)
        {
            if(ordenador == null || id != ordenador.Id)
                return BadRequest();

            var ordenadorAEditar = _repositorioOrdenador.TomaOrdenador(id);
            if (ordenadorAEditar == null)
                return NotFound();

            _repositorioOrdenador.UpdateOrdenador(ordenador);
            return NoContent();
        }

        // DELETE api/<OrdenadoresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ordenadorAEliminar = _repositorioOrdenador.TomaOrdenador(id);
            if(ordenadorAEliminar == null)
                return NotFound();

            _repositorioOrdenador.BorraOrdenador(id);
            return NoContent();
        }
    }
}
