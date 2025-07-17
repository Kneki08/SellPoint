using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sellpoint.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _ProductoRepository;
        public ProductoController(IProductoRepository ProductoRepository)
        {
            _ProductoRepository = ProductoRepository;
        }
        // GET: api/<PedidoController>
        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _ProductoRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _ProductoRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<PedidoController>
        [HttpPost("SaveProductoDTO")]
        public async Task<IActionResult> Post([FromBody] SaveProductoDTO saveProductoDTO)
        {
            var result = await _ProductoRepository.AgregarAsync(saveProductoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // PUT api/<PedidoController>/5
        [HttpPost("UpdateProductoDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateProductoDTO updateProductoDTO)
        {
            var result = await _ProductoRepository.ActualizarAsync(updateProductoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // DELETE api/<PedidoController>/5
        [HttpPost("RemoveProductoDTO")]
        public async Task<IActionResult> Put([FromBody] RemoveProductoDTO removeProductoDTO)
        {
            var result = await _ProductoRepository.EliminarAsync(removeProductoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}
