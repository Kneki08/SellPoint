using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.IService;

namespace Sellpoint.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var result = await _pedidoService.ObtenerTodosAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _pedidoService.ObtenerPorIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SavePedidoDTO dto)
        {
            var result = await _pedidoService.AgregarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePedidoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var result = await _pedidoService.ActualizarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = new RemovePedidoDTO { Id = id };
            var result = await _pedidoService.EliminarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}