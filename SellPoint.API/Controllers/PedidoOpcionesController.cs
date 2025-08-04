using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SellPoint.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoOpcionesController : ControllerBase
    {
        [HttpGet("usuarios")]
        public ActionResult<List<int>> ObtenerUsuarios()
        {
            // Mock temporal - en el futuro vendrá de la API de Usuarios
            var usuarios = new List<int> { 2, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            return Ok(usuarios);
        }

        [HttpGet("direcciones")]
        public ActionResult<List<int>> ObtenerDirecciones()
        {
            // Mock temporal - en el futuro vendrá de la API de Direcciones
            var direcciones = new List<int> { 1, 2, 3 };
            return Ok(direcciones);
        }

        [HttpGet("metodos-pago")]
        public ActionResult<List<string>> ObtenerMetodosPago()
        {
            var metodos = new List<string> { "PayPal", "TransferenciaBancaria", "Tarjeta" };
            return Ok(metodos);
        }

        [HttpGet("estados-pedido")]
        public ActionResult<List<string>> ObtenerEstadosPedido()
        {
            var estados = new List<string> { "EnPreparacion", "Enviado", "Entregado", "Cancelado" };
            return Ok(estados);
        }
    }
}