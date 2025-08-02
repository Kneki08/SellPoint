using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.View.Models.ModelsProducto;
using SellPoint.View.Service.ServiceApiProducto;

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

        var dto = new SaveProductoDTO
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

        var dto = new UpdateProductoDTO
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
        return await _productoApiClient.EliminarAsync(new RemoveProductoDTO { Id = id });
    }

    public async Task<List<ProductoModel>> ObtenerTodosAsync()
    {
        var productos = await _productoApiClient.ObtenerTodosAsync();
        return productos.Select(dto => new ProductoModel
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

