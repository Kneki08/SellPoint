using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace Sellpoint.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponRepository _cuponRepository;

        public CuponController(ICuponRepository cuponRepository)
        {
            _cuponRepository = cuponRepository;
        }

        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _cuponRepository.ObtenerTodosAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _cuponRepository.ObtenerPorIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveCuponDTO dto)
        {
            var result = await _cuponRepository.AgregarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCuponDTO dto)
        {
            var result = await _cuponRepository.ActualizarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemoveCuponDTIO dto)
        {
            var result = await _cuponRepository.EliminarAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

