
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Base;

namespace SellPoint.Persistence.Repositories
{
    public class ProductoRepository : BaseRepository<ProductoRepository>, IProductoRepository
    {
        public ProductoRepository(string connectionString, ILogger<ProductoRepository> logger)
            : base(connectionString, logger) { }

        public async Task<OperationResult> AgregarAsync(SaveProductoDTO entidad)
        {
            if (entidad is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            return await ExecuteNonQueryAsync("sp_AgregarProducto", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion ?? string.Empty);
                cmd.Parameters.AddWithValue("@Precio", entidad.Precio);
                cmd.Parameters.AddWithValue("@Stock", entidad.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ImagenUri", entidad.ImagenUri ?? string.Empty);
                cmd.Parameters.AddWithValue("@Activo", entidad.Activo);
            });
        }

        public async Task<OperationResult> ActualizarAsync(UpdateProductoDTO entidad)
        {
            if (entidad is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            return await ExecuteNonQueryAsync("sp_ActualizarProducto", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", entidad.Id);
                cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion ?? string.Empty);
                cmd.Parameters.AddWithValue("@Precio", entidad.Precio);
                cmd.Parameters.AddWithValue("@Stock", entidad.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", entidad.CategoriaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ImagenUri", entidad.ImagenUri ?? string.Empty);
                cmd.Parameters.AddWithValue("@Activo", entidad.Activo);
            });
        }

        public async Task<OperationResult> EliminarAsync(RemoveProductoDTO entidad)
        {
            if (entidad is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            return await ExecuteNonQueryAsync("sp_EliminarProducto", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", entidad.Id);
                cmd.Parameters.AddWithValue("@EsEliminado", entidad.EsEliminado);
            });
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult.Failure("Id inválido.");

            return await ExecuteReaderAsync("sp_ObtenerProductoPorId", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
            },
            reader =>
            {
                if (!reader.HasRows) return null;

                reader.Read();

                return new ProductoDTO
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
            });
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            return await ExecuteReaderAsync("sp_ObtenerTodosProductos", cmd => { }, reader =>
            {
                var productos = new List<ProductoDTO>();

                while (reader.Read())
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

                return productos;
            });
        }
    }
}

