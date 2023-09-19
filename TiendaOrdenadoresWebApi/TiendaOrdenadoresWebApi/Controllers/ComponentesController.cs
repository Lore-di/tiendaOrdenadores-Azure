using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaOrdenadoresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentesController : ControllerBase
    {

        private readonly IRepositorioComponente _repositorioComponente;

        public ComponentesController (IRepositorioComponente repositorioComponente)
        {
            _repositorioComponente = repositorioComponente;
        }


        // GET: api/<ComponentesController>
        [HttpGet]
        public IActionResult Get()
        {
            var componentes = _repositorioComponente.ListaComponentes();
            return Ok(componentes);
        }

        // GET api/<ComponentesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var componente = _repositorioComponente.TomaComponente(id);
            if (componente == null)
                return NotFound();
            return Ok(componente) ;
        }

        // POST api/<ComponentesController>
        [HttpPost]
        public IActionResult Post([FromBody] Componente componente)
        {
            _repositorioComponente.AddComponente(componente);
            return CreatedAtAction(nameof(Get), new { id = componente.Id }, componente);
        }
    

        // PUT api/<ComponentesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Componente componente)
        {
            if(id != componente.Id)
                return BadRequest();

            var componenteAEditar = _repositorioComponente.TomaComponente(id);
            if (componenteAEditar == null)
                return NotFound();

            _repositorioComponente.UpdateComponente(componenteAEditar);
            return NoContent();
        }

        // DELETE api/<ComponentesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var componenteABorrar = _repositorioComponente.TomaComponente(id);

            if(componenteABorrar == null)
                return NotFound();

            _repositorioComponente.BorraComponente(id);
            return NoContent();

        }
    }
}
