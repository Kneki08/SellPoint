using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Validations.PedidoValidator;
using SellPoint.Aplication.Validations.Mensajes;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<PedidoRepository> _logger;

        public PedidoRepository(string connectionString, ILogger<PedidoRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> AgregarAsync(Pedido pedido)
        {
            var validacion = PedidoValidator.ValidarEntidad(pedido);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("AgregarAsync {UsuarioId}", pedido.IdUsuario);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_AgregarPedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UsuarioId", pedido.IdUsuario);
                command.Parameters.AddWithValue("@Subtotal", pedido.Subtotal);
                command.Parameters.AddWithValue("@Descuento", pedido.Descuento);
                command.Parameters.AddWithValue("@CostoEnvio", pedido.CostoEnvio);
                command.Parameters.AddWithValue("@Total", pedido.Total);
                command.Parameters.AddWithValue("@MetodoPago", pedido.MetodoPago.ToString());
                command.Parameters.AddWithValue("@ReferenciaPago", pedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CuponId", pedido.CuponId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DireccionEnvioId", pedido.DireccionEnvioId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notas", pedido.Notas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaCreacion", pedido.Fecha_creacion);

                await context.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al agregar un pedido. Verifica el SP.");

                return OperationResult.Success(
                    rowsAffected > 0,
                    rowsAffected > 0 ? MensajesValidacion.PedidoAgregado : MensajesValidacion.PedidoNoAgregado
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el pedido");
                return OperationResult.Failure(MensajesValidacion.ErrorAgregarPedido);
            }
        }

        public async Task<OperationResult> ActualizarAsync(Pedido pedido)
        {
            var validacion = PedidoValidator.ValidarEntidad(pedido, true);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("ActualizarAsync UsuarioId: {IdUsuario}", pedido.IdUsuario);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarPedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UsuarioId", pedido.IdUsuario);
                command.Parameters.AddWithValue("@Estado", pedido.Estado.ToString());
                command.Parameters.AddWithValue("@MetodoPago", pedido.MetodoPago.ToString());
                command.Parameters.AddWithValue("@ReferenciaPago", pedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notas", pedido.Notas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaActualizacion", pedido.Fecha_actualizacion ?? (object)DBNull.Value);

                await context.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al actualizar un pedido. Verifica el SP.");

                return OperationResult.Success(
                    rowsAffected > 0,
                    rowsAffected > 0 ? MensajesValidacion.PedidoActualizado : MensajesValidacion.PedidoNoActualizado
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido");
                return OperationResult.Failure(MensajesValidacion.ErrorActualizarPedido);
            }
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            var validacion = PedidoValidator.ValidarRemove(removePedido);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("EliminarAsync {Id}", removePedido.Id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarPedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", removePedido.Id);

                await context.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();

                if (result > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al eliminar un pedido. Verifica el SP.");

                return OperationResult.Success(
                    result > 0,
                    result > 0 ? MensajesValidacion.PedidoEliminado : MensajesValidacion.PedidoNoEliminado
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido");
                return OperationResult.Failure(MensajesValidacion.ErrorEliminarPedido);
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            var validacion = PedidoValidator.ValidarId(id);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerPedidoPorId", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var pedido = new PedidoDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        IdUsuario = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                        FechaPedido = reader.GetDateTime(reader.GetOrdinal("FechaPedido")),
                        Estado = reader["Estado"] as string ?? string.Empty,
                        IdDireccionEnvio = reader["DireccionEnvioId"] != DBNull.Value
                            ? reader.GetInt32(reader.GetOrdinal("DireccionEnvioId"))
                            : 0,
                        CuponId = reader["CuponId"] != DBNull.Value
                            ? reader.GetInt32(reader.GetOrdinal("CuponId"))
                            : null,
                        MetodoPago = reader["MetodoPago"] as string ?? string.Empty,
                        ReferenciaPago = reader["ReferenciaPago"] as string ?? string.Empty,
                        NumeroPedido = reader["NumeroPedido"] as string ?? string.Empty,
                        Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                        Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                        CostoEnvio = reader.GetDecimal(reader.GetOrdinal("CostoEnvio")),
                        Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                        Notas = reader["Notas"] as string ?? string.Empty
                    };

                    return OperationResult.Success(pedido, MensajesValidacion.PedidoObtenido);
                }
                else
                {
                    return OperationResult.Failure(MensajesValidacion.PedidoNoEncontradoSimple);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido por Id");
                return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidoPorId);
            }
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            try
            {
                _logger.LogInformation("ObtenerTodosAsync llamado");

                var pedidos = new List<PedidoDTO>();

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerTodosPedidos", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var pedido = new PedidoDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        IdUsuario = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                        FechaPedido = reader.GetDateTime(reader.GetOrdinal("FechaPedido")),
                        Estado = reader["Estado"] as string ?? string.Empty,
                        IdDireccionEnvio = reader["DireccionEnvioId"] != DBNull.Value
                            ? reader.GetInt32(reader.GetOrdinal("DireccionEnvioId"))
                            : 0,
                        CuponId = reader["CuponId"] != DBNull.Value
                            ? reader.GetInt32(reader.GetOrdinal("CuponId"))
                            : null,
                        MetodoPago = reader["MetodoPago"] as string ?? string.Empty,
                        ReferenciaPago = reader["ReferenciaPago"] as string ?? string.Empty,
                        NumeroPedido = reader["NumeroPedido"] as string ?? string.Empty,
                        Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                        Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                        CostoEnvio = reader.GetDecimal(reader.GetOrdinal("CostoEnvio")),
                        Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                        Notas = reader["Notas"] as string ?? string.Empty
                    };

                    pedidos.Add(pedido);
                }

                if (pedidos.Any())
                {
                    return OperationResult.Success(pedidos, MensajesValidacion.PedidosObtenidosCorrectamente);
                }
                else
                {
                    return OperationResult.Failure(MensajesValidacion.ErrorObtenerPedidos);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los pedidos");
                return OperationResult.Failure(MensajesValidacion.ErrorObtenerTodos);
            }
        }
    }
}