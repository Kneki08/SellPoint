using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
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

        public async Task<OperationResult> AgregarAsync(SavePedidoDTO savePedido)
        {
            OperationResult Presult = OperationResult.Success();

            try
            {
                if (savePedido is null)
                    return OperationResult.Failure("La entidad no puede ser nula.");

                if (savePedido.UsuarioId <= 0)
                    return OperationResult.Failure("El UsuarioId debe ser mayor que cero.");

                if (savePedido.Total != (savePedido.Subtotal - savePedido.Descuento + savePedido.CostoEnvio))
                    return OperationResult.Failure("El total no coincide con la suma de subtotal, descuento y costo de envío.");

                if (!string.IsNullOrWhiteSpace(savePedido.MetodoPago) && savePedido.MetodoPago.Length > 50)
                    return OperationResult.Failure("El método de pago no debe superar los 50 caracteres.");

                if (!string.IsNullOrWhiteSpace(savePedido.ReferenciaPago) && savePedido.ReferenciaPago.Length > 100)
                    return OperationResult.Failure("La referencia de pago no debe superar los 100 caracteres.");

                if (!string.IsNullOrWhiteSpace(savePedido.Notas) && savePedido.Notas.Length > 500)
                    return OperationResult.Failure("Las notas no deben superar los 500 caracteres.");

                _logger.LogInformation("AgregarAsync {UsuarioId}", savePedido.UsuarioId);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_AgregarPedido", context)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UsuarioId", savePedido.UsuarioId);
                command.Parameters.AddWithValue("@Subtotal", savePedido.Subtotal);
                command.Parameters.AddWithValue("@Descuento", savePedido.Descuento);
                command.Parameters.AddWithValue("@CostoEnvio", savePedido.CostoEnvio);
                command.Parameters.AddWithValue("@Total", savePedido.Total);
                command.Parameters.AddWithValue("@MetodoPago", savePedido.MetodoPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReferenciaPago", savePedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CuponId", savePedido.CuponId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DireccionEnvioId", savePedido.DireccionEnvioId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notas", savePedido.Notas ?? (object)DBNull.Value);

                await context.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();

                if (result > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al agregar un pedido. Verifica el SP.");

                Presult.IsSuccess = result > 0;
                Presult.Message = result > 0
                    ? "Pedido agregado correctamente."
                    : "No se pudo agregar el pedido.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el pedido");
                return OperationResult.Failure("Error al agregar el pedido");
            }

            return Presult;
        }

        public async Task<OperationResult> ActualizarAsync(UpdatePedidoDTO updatePedido)
        {
            OperationResult Presult = OperationResult.Success();

            try
            {
                if (updatePedido is null)
                    return OperationResult.Failure("La entidad no puede ser nula.");

                if (updatePedido.Id <= 0)
                    return OperationResult.Failure("El Id del pedido debe ser mayor que cero.");

                if (updatePedido.FechaActualizacion == DateTime.MinValue || updatePedido.FechaActualizacion > DateTime.Now.AddMinutes(5))
                    return OperationResult.Failure("La fecha de actualización no es válida.");

                if (!string.IsNullOrWhiteSpace(updatePedido.MetodoPago) && updatePedido.MetodoPago.Length > 50)
                    return OperationResult.Failure("El método de pago no debe superar los 50 caracteres.");

                if (!string.IsNullOrWhiteSpace(updatePedido.ReferenciaPago) && updatePedido.ReferenciaPago.Length > 100)
                    return OperationResult.Failure("La referencia de pago no debe superar los 100 caracteres.");

                if (!string.IsNullOrWhiteSpace(updatePedido.Notas) && updatePedido.Notas.Length > 500)
                    return OperationResult.Failure("Las notas no deben superar los 500 caracteres.");

                _logger.LogInformation("ActualizarAsync {Id}", updatePedido.Id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarPedido", context)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", updatePedido.Id);
                command.Parameters.AddWithValue("@Estado", updatePedido.Estado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@MetodoPago", updatePedido.MetodoPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReferenciaPago", updatePedido.ReferenciaPago ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notas", updatePedido.Notas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaActualizacion", updatePedido.FechaActualizacion);

                await context.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();

                if (result > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al actualizar un pedido. Verifica el SP.");

                Presult.IsSuccess = result > 0;
                Presult.Message = result > 0
                    ? "Pedido actualizado correctamente."
                    : "No se pudo actualizar el pedido.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido");
                return OperationResult.Failure("Error al actualizar el pedido");
            }

            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            OperationResult Presult = OperationResult.Success();

            try
            {
                if (removePedido is null)
                    return OperationResult.Failure("La entidad no puede ser nula.");

                if (removePedido.Id <= 0)
                    return OperationResult.Failure("El Id del pedido debe ser mayor que cero.");

                _logger.LogInformation("EliminarAsync {Id}", removePedido.Id);

                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarPedido", context)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", removePedido.Id);

                await context.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();

                if (result > 1)
                    _logger.LogWarning("Se afectaron múltiples filas al eliminar un pedido. Verifica el SP.");

                Presult.IsSuccess = result > 0;
                Presult.Message = result > 0
                    ? "Pedido eliminado correctamente."
                    : "No se pudo eliminar el pedido.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido");
                return OperationResult.Failure("Error al eliminar el pedido");
            }

            return Presult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (id <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id debe ser mayor que cero.";
                    return Presult;
                }

                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ObtenerPedidoPorId", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await context.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var pedido = new PedidoDTO
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                                    Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                                    CostoEnvio = reader.GetDecimal(reader.GetOrdinal("CostoEnvio")),
                                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                    MetodoPago = reader["MetodoPago"] as string,
                                    ReferenciaPago = reader["ReferenciaPago"] as string,
                                    CuponId = reader["CuponId"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("CuponId")) : (int?)null,
                                    DireccionEnvioId = reader["DireccionEnvioId"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("DireccionEnvioId")) : (int?)null,
                                    Notas = reader["Notas"] as string,
                                    Estado = reader["Estado"] as string
                                };

                                Presult.IsSuccess = true;
                                Presult.Data = pedido;
                                Presult.Message = "Pedido obtenido correctamente.";
                            }
                            else
                            {
                                Presult.IsSuccess = false;
                                Presult.Message = "No se encontró el pedido.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido por Id");
                return OperationResult.Failure("Error al obtener el pedido por Id");
            }

            return Presult;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult Presult = OperationResult.Success();

            try
            {
                _logger.LogInformation("ObtenerTodosAsync llamado");

                var pedidos = new List<PedidoDTO>();

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ObtenerTodosPedidos", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await context.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var pedido = new PedidoDTO
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                                    Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                                    CostoEnvio = reader.GetDecimal(reader.GetOrdinal("CostoEnvio")),
                                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                    MetodoPago = reader["MetodoPago"] as string,
                                    ReferenciaPago = reader["ReferenciaPago"] as string,
                                    CuponId = reader["CuponId"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("CuponId")) : (int?)null,
                                    DireccionEnvioId = reader["DireccionEnvioId"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("DireccionEnvioId")) : (int?)null,
                                    Notas = reader["Notas"] as string,
                                    Estado = reader["Estado"] as string
                                };

                                pedidos.Add(pedido);
                            }
                        }

                        if (pedidos.Any())
                        {
                            Presult.IsSuccess = true;
                            Presult.Data = pedidos;
                            Presult.Message = "Lista de pedidos obtenida correctamente.";
                        }
                        else
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se encontraron pedidos.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los pedidos");
                return OperationResult.Failure("Error al obtener todos los pedidos");
            }

            return Presult;
        }
    }
}
