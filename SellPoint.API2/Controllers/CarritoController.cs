﻿using Microsoft.AspNetCore.Mvc;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace SellPoint.API2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoRepository _CarritoRepository;
        public CarritoController(ICarritoRepository CarritoRepository)
        {
            _CarritoRepository = CarritoRepository;
        }
        // GET: api/<PedidoController>
        [HttpGet("ObtenerTodosAsync")]
        public async Task<IActionResult> Get()
        {
            var result = await _CarritoRepository.ObtenerTodosAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _CarritoRepository.ObtenerPorIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }
        }

        // POST api/<PedidoController>
        [HttpPost("SaveCarritoDTO")]
        public async Task<IActionResult> Post([FromBody] SaveCarritoDTO saveCarritoDTO)
        {
            var result = await _CarritoRepository.AgregarAsync(saveCarritoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // PUT api/<PedidoController>/5
        [HttpPost("UpdateCarritoDTO")]
        public async Task<IActionResult> Put([FromBody] UpdateCarritoDTO updateCarritoDTO)
        {
            var result = await _CarritoRepository.ActualizarAsync(updateCarritoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }


        }

        // DELETE api/<PedidoController>/5
        [HttpPost("RemoveCarritoDTO")]
        public async Task<IActionResult> Put([FromBody] RemoveCarritoDTO removeCarritoDTO)
        {
            var result = await _CarritoRepository.EliminarAsync(removeCarritoDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else { return BadRequest(result); }

        }
    }
}
