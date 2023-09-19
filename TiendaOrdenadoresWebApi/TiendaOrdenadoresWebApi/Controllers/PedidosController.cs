using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Models;
using TiendaOrdenadoresWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaOrdenadoresWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IRepositorioPedido _repositorioPedido;

        public PedidosController(IRepositorioPedido repositorioPedido)
        {
            _repositorioPedido = repositorioPedido;
        }


        // GET: api/<PedidosController>
        [HttpGet]
        public IActionResult Get()
        {
            var pedidos = _repositorioPedido.ListaPedido();
            if(pedidos == null)
                return NotFound();

            return Ok(pedidos);
        }

        // GET api/<PedidosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pedido = _repositorioPedido.TomaPedido(id);
            if(pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        // POST api/<PedidosController>
        [HttpPost]
        public IActionResult Post([FromBody] Pedido pedido)
        {
            if(pedido == null)
                return BadRequest();

            _repositorioPedido.UpdatePedido(pedido);
            return CreatedAtAction(nameof(Get), new { Id = pedido.Id }, pedido);
        }

        // PUT api/<PedidosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pedido pedido)
        {
            if(id != pedido.Id)
                return BadRequest();
            var pedidoAEditar = _repositorioPedido.TomaPedido(id);
            if (pedidoAEditar == null)
                return NotFound();
            _repositorioPedido.UpdatePedido(pedido);
            return NoContent();
        }

        // DELETE api/<PedidosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pedidoAEliminar = _repositorioPedido.TomaPedido(id);
            if(pedidoAEliminar == null)
                return NotFound();
            _repositorioPedido.BorraPedido(id);
            return NoContent();

        }
    }
}
