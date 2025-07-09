using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace SellPoint.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoRepository _detallePedidoRepository;
        public DetallePedidoController(IDetallePedidoRepository detallePedidoRepository)
        {
            _detallePedidoRepository = detallePedidoRepository;
        }


        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _detallePedidoRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // GET api/<DetallePedidoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _detallePedidoRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<DetallePedidoController>
        [HttpPost("SaveDetallePedidoDTO")]
        public async Task<IActionResult> Post([FromBody] SaveDetallePedidoDTO saveDetallePedidoDTO)
        {
            var result = await _detallePedidoRepository.AgregarAsync(saveDetallePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // PUT api/<DetallePedidoController>/5
        [HttpPost("UpdateDetallePedidoDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateDetallePedidoDTO updateDetallePedidoDTO)
        {
            var result = await _detallePedidoRepository.ActualizarAsync(updateDetallePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }

        // DELETE api/<DetallePedidoController>/5
        [HttpPost("RemoveDetallePedidoDTO")]
        public async Task<IActionResult> Put([FromBody] RemoveDetallePedidoDTO removeDetallePedidoDTO)
        {
            var result = await _detallePedidoRepository.EliminarAsync(removeDetallePedidoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}
