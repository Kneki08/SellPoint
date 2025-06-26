using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Persistence.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(string connectionString,ILogger<ProductoRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateProductoDTO entidad)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (entidad is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                _logger.LogInformation("ActualizarAsync {Id}", entidad.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarProducto", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", entidad.Id);
                command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                command.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                command.Parameters.AddWithValue("@Precio", entidad.Precio);
                command.Parameters.AddWithValue("@Stock", entidad.Stock);
                command.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ImagenUri", entidad.ImagenUri);
                command.Parameters.AddWithValue("@Activo", entidad.Activo);

                await connection.OpenAsync();
                var rows = await command.ExecuteNonQueryAsync();

                Presult.IsSuccess = rows > 0;
                Presult.Message = rows > 0 ? "Producto actualizado correctamente." : "No se pudo actualizar el producto.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto", ex);
            }

            return Presult;
        }

        public async Task<OperationResult> AgregarAsync(SaveProductoDTO entidad)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (entidad is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                _logger.LogInformation("AgregarAsync {Nombre}", entidad.Nombre);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_AgregarProducto", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                command.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                command.Parameters.AddWithValue("@Precio", entidad.Precio);
                command.Parameters.AddWithValue("@Stock", entidad.Stock);
                command.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ImagenUri", entidad.ImagenUri);
                command.Parameters.AddWithValue("@Activo", entidad.Activo);

                await connection.OpenAsync();
                var rows = await command.ExecuteNonQueryAsync();

                Presult.IsSuccess = rows > 0;
                Presult.Message = rows > 0 ? "Producto agregado correctamente." : "No se pudo agregar el producto.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto", ex);
            }

            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveProductoDTO entidad)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (entidad is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                _logger.LogInformation("EliminarAsync {Id}", entidad.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarProducto", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", entidad.Id);
                command.Parameters.AddWithValue("@EsEliminado", entidad.EsEliminado);

                await connection.OpenAsync();
                var rows = await command.ExecuteNonQueryAsync();

                Presult.IsSuccess = rows > 0;
                Presult.Message = rows > 0 ? "Producto eliminado correctamente." : "No se pudo eliminar el producto.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto", ex);
            }

            return Presult;
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
