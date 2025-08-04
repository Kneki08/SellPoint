using SellPoint.View.DTOS.CarritoDTOS;
using SellPoint.View.Models.ModelsCarito;
using SellPoint.View.Models.ModelsCarrito;
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
            if (!Validar(model, out string mensaje))
            {
                MostrarMensaje(mensaje);
                return false;
            }

            var saveModel = new SaveCarritoModel
            {
                ProductoId = model.ProductoId,
                Cantidad = model.Cantidad,
                UsuarioId = model.UsuarioId
            };

            return await _carritoApiClient.CrearAsync(saveModel);
        }

        public async Task<bool> ActualizarAsync(CarritoModel model)
        {
            if (!Validar(model, out string mensaje))
            {
                MostrarMensaje(mensaje);
                return false;
            }

            var updateModel = new UpdateCarritoModel
            {
                Id = model.Id,
                ProductoId = model.ProductoId,
                Cantidad = model.Cantidad,
                UsuarioId = model.UsuarioId,
                FechaActualizacion = DateTime.Now
            };

            return await _carritoApiClient.ActualizarAsync(updateModel);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var removeModel = new RemoveCarritoModel { Id = id };
            return await _carritoApiClient.EliminarAsync(removeModel);
        }

        public async Task<List<CarritoModel>> ObtenerTodosAsync()
        {
            return await _carritoApiClient.ObtenerTodosAsync();
        }

        public bool Validar(CarritoModel model, out string mensaje) // Changed to public
        {
            if (model.UsuarioId <= 0)
            {
                mensaje = "El ID del usuario es obligatorio y debe ser mayor a cero.";
                return false;
            }

            if (model.ProductoId <= 0)
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

        private void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}


