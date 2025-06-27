using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;
using System.Data;

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

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerProductoPorId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var producto = new ProductoDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                        Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                        CategoriaId = reader.IsDBNull(reader.GetOrdinal("CategoriaId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CategoriaId")),
                        ImagenUri = reader.IsDBNull(reader.GetOrdinal("ImagenUri")) ? null : reader.GetString(reader.GetOrdinal("ImagenUri")),
                        Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                    };

                    Presult.Data = producto;
                    Presult.IsSuccess = true;
                }
                else
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "Producto no encontrado.";
                }
            }
            catch (Exception ex)
            {
                Presult.IsSuccess = false;
                Presult.Message = $"Error al obtener el producto: {ex.Message}";
                _logger.LogError(ex, "Error en ObtenerPorIdAsync");
            }

            return Presult;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                _logger.LogInformation("ObtenerTodosAsync");

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerTodosProductos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                var productos = new List<ProductoDTO>();
                while (await reader.ReadAsync())
                {
                    var producto = new ProductoDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                        Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                        CategoriaId = reader.IsDBNull(reader.GetOrdinal("CategoriaId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CategoriaId")),
                        ImagenUri = reader.IsDBNull(reader.GetOrdinal("ImagenUri")) ? null : reader.GetString(reader.GetOrdinal("ImagenUri")),
                        Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                    };

                    productos.Add(producto);
                }

                Presult.Data = productos;
                Presult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Presult.IsSuccess = false;
                Presult.Message = $"Error al obtener los productos: {ex.Message}";
                _logger.LogError(ex, "Error en ObtenerTodosAsync");
            }

            return Presult;
        }
    }
}
