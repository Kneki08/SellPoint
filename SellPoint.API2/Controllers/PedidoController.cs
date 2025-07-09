using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace SellPoint.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _PedidoRepository;
        public PedidoController(IPedidoRepository PedidoRepository)
        {
            _PedidoRepository = PedidoRepository;
        }
        // GET: api/<PedidoController>
        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _PedidoRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _PedidoRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<PedidoController>
        [HttpPost("SavePedidoDTO")]
        public async Task<IActionResult> Post([FromBody] SavePedidoDTO savePedidoDTO)
        {
            var result = await _PedidoRepository.AgregarAsync(savePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // PUT api/<PedidoController>/5
        [HttpPost("UpdatePedidoDTO")]
        public async Task<IActionResult> Put([FromBody] UpdatePedidoDTO updatePedidoDTO)
        {
            var result = await _PedidoRepository.ActualizarAsync(updatePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // DELETE api/<PedidoController>/5
        [HttpPost("RemovePedidoDTO")]
        public async Task<IActionResult> Put([FromBody] RemovePedidoDTO removePedidoDTO)
        {
            var result = await _PedidoRepository.EliminarAsync(removePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}
