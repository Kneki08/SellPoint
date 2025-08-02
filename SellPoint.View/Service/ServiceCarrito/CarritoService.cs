using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Service.ServiceApiCarrito;

namespace SellPoint.View.Service.ServiceCarrito
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoApiClient _carritoApiClient;

        public CarritoService(ICarritoApiClient carritoApiClient)
        {
            _carritoApiClient = carritoApiClient;
        }

        public async Task<bool> AgregarAsync(CarritoModel model)
        {
            if (!ValidarFormulario(model, out _)) return false;

            var dto = new SaveCarritoDTO
            {
                ProductoId = model.IdProducto,
                Cantidad = model.Cantidad,
                ClienteId = (int)model.ClienteId
            };

            return await _carritoApiClient.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(CarritoModel model)
        {
            if (!ValidarFormulario(model, out _)) return false;

            var dto = new UpdateCarritoDTO
            {
                Id = model.Id,
                ProductoId = model.IdProducto,
                NuevaCantidad = model.Cantidad,
                ClienteId = (int)model.ClienteId
            };

            return await _carritoApiClient.ActualizarAsync(dto);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var dto = new RemoveCarritoDTO { Id = id };
            return await _carritoApiClient.EliminarAsync(dto);
        }

        public async Task<List<CarritoModel>> ObtenerTodosAsync()
        {
            var dtos = await _carritoApiClient.ObtenerTodosAsync();

            return dtos.Select(dto => new CarritoModel
            {
                Id = dto.Id,
                IdProducto = dto.ProductoId,
                Cantidad = dto.Cantidad,
                ClienteId = dto.ClienteId,
                PrecioUnitario = dto.Precio,
                Activo = (bool)dto.Estado,
                FechaAgregado = dto.FechaAgregado,
                Estado = dto.Estado
            }).ToList();
        }

        public bool ValidarFormulario(CarritoModel model, out string mensaje)
        {
            if (model.ClienteId == null || Convert.ToInt32(model.ClienteId) <= 0)
            {
                mensaje = "El ID del cliente es obligatorio y debe ser mayor a cero.";
                return false;
            }

            if (model.IdProducto <= 0)
            {
                mensaje = "El ID del producto es obligatorio.";
                return false;
            }

            if (model.Cantidad <= 0)
            {
                mensaje = "La cantidad debe ser mayor a cero.";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }
    }
}

