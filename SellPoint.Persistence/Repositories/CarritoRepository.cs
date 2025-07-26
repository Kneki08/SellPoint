
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Base;   

namespace SellPoint.Persistence.Repositories
{
    public class CarritoRepository : BaseRepository<CarritoRepository>, ICarritoRepository
    {
        public CarritoRepository(string connectionString, ILogger<CarritoRepository> logger)
            : base(connectionString, logger) { }

        public async Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarritoDTO)
        {
            if (saveCarritoDTO == null || saveCarritoDTO.Cantidad <= 0)
                return OperationResult.Failure("Datos inválidos");

            return await ExecuteNonQueryAsync("sp_AgregarCarrito", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioId", saveCarritoDTO.ClienteId);
                cmd.Parameters.AddWithValue("@ProductoId", saveCarritoDTO.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", saveCarritoDTO.Cantidad);
            });
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarritoDTO)
        {
            if (updateCarritoDTO == null || updateCarritoDTO.NuevaCantidad <= 0)
                return OperationResult.Failure("Datos inválidos");

            return await ExecuteNonQueryAsync("sp_ActualizarCarrito", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioId", updateCarritoDTO.UsuarioId);
                cmd.Parameters.AddWithValue("@ProductoId", updateCarritoDTO.ProductoId);
                cmd.Parameters.AddWithValue("@NuevaCantidad", updateCarritoDTO.NuevaCantidad);
            });
        }

        public async Task<OperationResult> EliminarAsync(RemoveCarritoDTO removeCarritoDTO)
        {
            if (removeCarritoDTO == null)
                return OperationResult.Failure("Entidad nula.");

            return await ExecuteNonQueryAsync("sp_EliminarCarrito", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioId", removeCarritoDTO.UsuarioId);
                cmd.Parameters.AddWithValue("@ProductoId", removeCarritoDTO.ProductoId);
            });
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int usuarioId)
        {
            return await ExecuteReaderAsync("sp_ObtenerCarritoPorUsuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            },
            reader => new ObtenerCarritoDTO
            {
                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
            });
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            return await ExecuteReaderAsync("sp_ObtenerTodosLosCarritos", cmd => { },
            reader => new ObtenerCarritoDTO
            {
                ClienteId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
            });
        }
    }
}

