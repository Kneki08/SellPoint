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

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var result = await _pedidoService.ObtenerTodosAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _pedidoService.ObtenerPorIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AgregarPedido")]
        public async Task<IActionResult> Agregar([FromBody] SavePedidoDTO dto)
        {
            var result = await _pedidoService.AgregarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("ActualizarPedido")]
        public async Task<IActionResult> Actualizar([FromBody] UpdatePedidoDTO dto)
        {
            var result = await _pedidoService.ActualizarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("EliminarPedido")]
        public async Task<IActionResult> Eliminar([FromBody] RemovePedidoDTO dto)
        {
            var result = await _pedidoService.EliminarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}