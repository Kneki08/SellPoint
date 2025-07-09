using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace SellPoint.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _CategoriaRepository;
        public CategoriaController(ICategoriaRepository CategoriaRepository)
        {
            _CategoriaRepository = CategoriaRepository;
        }
        // GET: api/<PedidoController>
        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _CategoriaRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _CategoriaRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<PedidoController>
        [HttpPost("SaveCategoriaDTO")]
        public async Task<IActionResult> Post([FromBody] SaveCategoriaDTO saveCategoriaDTO)
        {
            var result = await _CategoriaRepository.AgregarAsync(saveCategoriaDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // PUT api/<PedidoController>/5
        [HttpPost("UpdateCategoriaDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateCategoriaDTO updateCategoriaDTO)
        {
            var result = await _CategoriaRepository.ActualizarAsync(updateCategoriaDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // DELETE api/<PedidoController>/5
        [HttpPost("RemoveCategoriaDTO")]
        public async Task<IActionResult> Put([FromBody] RemoveCategoriaDTO removeCategoriaDTO)
        {
            var result = await _CategoriaRepository.EliminarAsync(removeCategoriaDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}
