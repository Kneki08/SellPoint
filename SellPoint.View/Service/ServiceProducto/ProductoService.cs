using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.View.DTOS.ProductoDTOS;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Models.ModelsProducto.SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Service.ServiceApiProducto;

namespace SellPoint.View.Service.ServiceProducto
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoApiClient _productoApiClient;

        public ProductoService(IProductoApiClient productoApiClient)
        {
            _productoApiClient = productoApiClient;
        }

        public async Task<bool> AgregarAsync(ProductoModel model)
        {
            if (!ValidarFormulario(model, out _)) return false;

            // Map SaveProductoDTO to SaveProductoModel
            var dto = new SaveProductoModel
            {
                Nombre = model.Nombre,
                Precio = model.Precio,
                Stock = model.Stock
            };

            return await _productoApiClient.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(ProductoModel model)
        {
            if (!ValidarFormulario(model, out _)) return false;

            // Map UpdateProductoDTO to UpdateProductoModel
            var dto = new UpdateProductoModel
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Precio = model.Precio,
                Stock = model.Stock
            };

            return await _productoApiClient.ActualizarAsync(dto);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            // Map RemoveProductoDTO to RemoveProductoModel
            var dto = new RemoveProductoModel { Id = id };
            return await _productoApiClient.EliminarAsync(dto);
        }

        public async Task<List<ProductoModel>> ObtenerTodosAsync()
        {
            var dtos = await _productoApiClient.ObtenerTodosAsync();

            return dtos.Select(dto => new ProductoModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock
            }).ToList();
        }

        public bool ValidarFormulario(ProductoModel model, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                mensaje = "El nombre del producto es obligatorio.";
                return false;
            }

            if (model.Precio <= 0)
            {
                mensaje = "El precio debe ser mayor que cero.";
                return false;
            }

            if (model.Stock < 0)
            {
                mensaje = "El stock no puede ser negativo.";
                return false;
            }

            mensaje = string.Empty;
            return true;
        }
    }
}


