using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sellpoint.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallepedidoService _detallePedidoService;
        public DetallePedidoController(IDetallepedidoService detallePedidoService)
        {
            _detallePedidoService = detallePedidoService;
        }


        [HttpGet ("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _detallePedidoService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET api/<DetallePedidoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _detallePedidoService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST api/<DetallePedidoController>
        [HttpPost("SaveDetallePedidoDTO")]
        public async Task<IActionResult> Post([FromBody] SaveDetallePedidoDTO dto)
        {
            // var result = await _detallePedidoRepository.AgregarAsync(saveDetallePedidoDTO);
            var result = await _detallePedidoService.AddAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        // PUT api/<DetallePedidoController>/5
        [HttpPost("UpdateDetallePedidoDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateDetallePedidoDTO dto)
        {
            var result = await _detallePedidoService.UpdateAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // DELETE api/<DetallePedidoController>/5
        [HttpPost("RemoveDetallePedidoDTO")]
        public async Task<IActionResult> Delete([FromBody] RemoveDetallePedidoDTO dto)
        {
            var result = await _detallePedidoService.DeleteAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
