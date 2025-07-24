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
                _logger.LogInformation("AgregarAsync iniciado para UsuarioId: {UsuarioId}", pedido.IdUsuario);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_CreatePedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Mapeo del enum al valor textual que espera la base de datos
                var estadoDb = pedido.Estado switch
                {
                    EstadoPedido.EnPreparacion => "En preparación",
                    EstadoPedido.Enviado => "Enviado",
                    EstadoPedido.Entregado => "Entregado",
                    EstadoPedido.Cancelado => "Cancelado",
                    _ => throw new ArgumentOutOfRangeException(nameof(pedido.Estado), "Estado no válido.")
                };

                // Parámetros con los nombres exactos que espera el SP
                command.Parameters.AddWithValue("@p_usuario_id", pedido.IdUsuario);
                command.Parameters.AddWithValue("@p_subtotal", pedido.Subtotal);
                command.Parameters.AddWithValue("@p_descuento", pedido.Descuento);
                command.Parameters.AddWithValue("@p_costo_envio", pedido.CostoEnvio);
                command.Parameters.AddWithValue("@p_total", pedido.Total);
                command.Parameters.AddWithValue("@p_estado", estadoDb);
                command.Parameters.AddWithValue("@p_metodo_pago", pedido.MetodoPago.ToString());
                command.Parameters.AddWithValue("@p_referencia_pago", pedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p_cupon_id", pedido.CuponId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p_direccion_envio_id", pedido.DireccionEnvioId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p_notas", pedido.Notas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@p_fecha_pedido", pedido.Fecha_creacion);

                await context.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 1)
                {
                    _logger.LogWarning("Se afectaron múltiples filas ({Rows}) al agregar un pedido. Verifica el SP.", rowsAffected);
                }

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("No se insertó ningún pedido.");
                    return OperationResult.Failure(MensajesValidacion.PedidoNoAgregado);
                }

                _logger.LogInformation("Pedido agregado correctamente.");
                return OperationResult.Success(true, MensajesValidacion.PedidoAgregado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar el pedido: {Mensaje}", ex.Message);
                return OperationResult.Failure($"Error inesperado al agregar el pedido. Detalle: {ex.Message}");
            }
        }

        public async Task<OperationResult> ActualizarAsync(Pedido pedido)
        {
            var validacion = PedidoValidator.ValidarEntidad(pedido, true);
            if (!validacion.IsSuccess)
            {
                _logger.LogWarning("Validación fallida en ActualizarAsync: {Mensaje}", validacion.Message);
                _logger.LogWarning("Datos recibidos: {@Pedido}", pedido);
                return validacion;
            }

            try
            {
                _logger.LogInformation("ActualizarAsync iniciado para UsuarioId: {IdUsuario}", pedido.IdUsuario);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarPedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", pedido.Id);
                command.Parameters.AddWithValue("@UsuarioId", pedido.IdUsuario);
                command.Parameters.AddWithValue("@Estado", pedido.Estado.ToString());
                command.Parameters.AddWithValue("@MetodoPago", pedido.MetodoPago.ToString());
                command.Parameters.AddWithValue("@ReferenciaPago", pedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notas", pedido.Notas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaActualizacion", pedido.Fecha_actualizacion ?? (object)DBNull.Value);

                await context.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("No se actualizó ningún pedido con ID: {Id}", pedido.Id);
                    return OperationResult.Success(false, MensajesValidacion.PedidoNoActualizado);
                }

                if (rowsAffected > 1)
                {
                    _logger.LogWarning("Se afectaron múltiples filas al actualizar el pedido. ID: {Id}, Filas: {Filas}", pedido.Id, rowsAffected);
                }

                _logger.LogInformation("Pedido actualizado correctamente. ID: {Id}", pedido.Id);
                return OperationResult.Success(true, MensajesValidacion.PedidoActualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el pedido. ID: {Id}", pedido.Id);
                return OperationResult.Failure($"{MensajesValidacion.ErrorActualizarPedido} Detalle: {ex.Message}");

            }
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            var validacion = PedidoValidator.ValidarRemove(removePedido);
            if (!validacion.IsSuccess)
            {
                _logger.LogWarning("Validación fallida en EliminarAsync: {Mensaje}", validacion.Message);
                _logger.LogWarning("Datos recibidos: {@RemovePedidoDTO}", removePedido);
                return validacion;
            }

            try
            {
                _logger.LogInformation("Iniciando eliminación de pedido. ID: {Id}", removePedido.Id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_DeletePedido", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@p_id", removePedido.Id);

                await context.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("No se eliminó ningún pedido. ID proporcionado: {Id}", removePedido.Id);
                    return OperationResult.Success(false, MensajesValidacion.PedidoNoEliminado);
                }

                if (rowsAffected > 1)
                {
                    _logger.LogWarning("Se eliminaron múltiples filas inesperadamente. ID: {Id}, Filas afectadas: {Filas}", removePedido.Id, rowsAffected);
                }

                _logger.LogInformation("Pedido eliminado correctamente. ID: {Id}", removePedido.Id);
                return OperationResult.Success(true, MensajesValidacion.PedidoEliminado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el pedido. ID: {Id}", removePedido.Id);
                return OperationResult.Failure(MensajesValidacion.ErrorEliminarPedido);
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            var validacion = PedidoValidator.ValidarId(id);
            if (!validacion.IsSuccess)
            {
                _logger.LogWarning("Validación fallida en ObtenerPorIdAsync. ID: {Id}. Motivo: {Mensaje}", id, validacion.Message);
                return validacion;
            }

            try
            {
                _logger.LogInformation("ObtenerPorIdAsync iniciado para ID: {Id}", id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_GetPedidoById", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@p_id", id); // <--- Asegúrate que coincida con el SP

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    _logger.LogInformation("Pedido encontrado. ID: {Id}", id);

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
                    _logger.LogWarning("No se encontró ningún pedido con ID: {Id}", id);
                    return OperationResult.Failure(MensajesValidacion.PedidoNoEncontradoSimple);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en ObtenerPorIdAsync para ID: {Id}", id);
                return OperationResult.Failure($"{MensajesValidacion.ErrorObtenerPedidoPorId}. Detalle: {ex.Message}");
            }
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            var pedidos = new List<PedidoDTO>();

            try
            {
                _logger.LogInformation("Conectando a SQL Server con cadena: {ConnStr}", _connectionString);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_GetAllPedidos", context)
                {
                    CommandType = CommandType.StoredProcedure
                };

                _logger.LogInformation("Ejecutando stored procedure sp_GetAllPedidos...");

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    try
                    {
                        var pedido = new PedidoDTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            IdUsuario = reader.GetInt32(reader.GetOrdinal("usuario_id")),
                            FechaPedido = reader.GetDateTime(reader.GetOrdinal("fecha_pedido")),
                            Estado = reader["estado"] as string ?? string.Empty,
                            IdDireccionEnvio = reader["direccion_envio_id"] != DBNull.Value
                                ? reader.GetInt32(reader.GetOrdinal("direccion_envio_id"))
                                : 0,
                            CuponId = reader["cupon_id"] != DBNull.Value
                                ? reader.GetInt32(reader.GetOrdinal("cupon_id"))
                                : null,
                            MetodoPago = reader["metodo_pago"] as string ?? string.Empty,
                            ReferenciaPago = reader["referencia_pago"] as string ?? string.Empty,
                            NumeroPedido = reader["numero_pedido"] as string ?? string.Empty,
                            Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                            Descuento = reader.GetDecimal(reader.GetOrdinal("descuento")),
                            CostoEnvio = reader.GetDecimal(reader.GetOrdinal("costo_envio")),
                            Total = reader.GetDecimal(reader.GetOrdinal("total")),
                            Notas = reader["notas"] as string ?? string.Empty
                        };

                        pedidos.Add(pedido);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error al leer un pedido del DataReader.");
                    }
                }

                _logger.LogInformation("Se obtuvieron {Count} pedidos.", pedidos.Count);

                if (pedidos.Any())
                {
                    return OperationResult.Success(pedidos, MensajesValidacion.PedidosObtenidosCorrectamente);
                }
                else
                {
                    _logger.LogWarning("No se encontraron pedidos en la base de datos.");
                    return OperationResult.Success(new List<PedidoDTO>(), MensajesValidacion.SinPedidosEncontrados);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general al obtener los pedidos.");
                return OperationResult.Failure($"No se pudieron obtener los pedidos. Detalle: {ex.Message}");
            }
        }
    }
}