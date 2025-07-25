using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Repositorios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sellpoint.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponRepository _CuponRepository;
        public CuponController(ICuponRepository CuponRepository)
        {
            _CuponRepository = CuponRepository;
        }
        // GET: api/<PedidoController>
        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _CuponRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _CuponRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<PedidoController>
        [HttpPost("SaveCuponDTO")]
        public async Task<IActionResult> Post([FromBody] SaveCuponDTO saveCuponDTO)
        {
            var result = await _CuponRepository.AgregarAsync(saveCuponDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // PUT api/<PedidoController>/5
        [HttpPost("UpdateCuponDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateCuponDTO updateCuponDTO)
        {
            var result = await _CuponRepository.ActualizarAsync(updateCuponDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // DELETE api/<PedidoController>/5
        [HttpPost("RemoveCuponDTO")]
        public async Task<IActionResult> Put([FromBody] RemoveCuponDTIO removeCuponDTO)
        {
            var result = await _CuponRepository.EliminarAsync(removeCuponDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}