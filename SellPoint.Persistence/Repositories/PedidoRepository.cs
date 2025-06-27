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

        public async Task<OperationResult> ActualizarAsync(UpdatePedidoDTO updatePedido)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (updatePedido is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (int.IsNegative(updatePedido.Id))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id del pedido no puede ser negativo.";
                    return Presult;
                }

                _logger.LogInformation("ActualizarAsync {Id}", updatePedido.Id);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ActualizarPedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", updatePedido.Id);
                        command.Parameters.AddWithValue("@Estado", updatePedido.Estado ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MetodoPago", updatePedido.MetodoPago ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ReferenciaPago", updatePedido.ReferenciaPago ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Notas", updatePedido.Notas ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FechaActualizacion", updatePedido.FechaActualizacion);

                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();

                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo actualizar el pedido.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Pedido actualizado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pedido", ex);
            }

            return Presult;
        }

        public async Task<OperationResult> AgregarAsync(SavePedidoDTO savePedido)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (savePedido is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (int.IsNegative(savePedido.UsuarioId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El UsuarioId no puede ser negativo.";
                    return Presult;
                }

                _logger.LogInformation("AgregarAsync {UsuarioId}", savePedido.UsuarioId);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_AgregarPedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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

                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo agregar el pedido.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Pedido agregado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el pedido", ex);
            }

            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemovePedidoDTO removePedido)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (removePedido is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (int.IsNegative(removePedido.Id))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id del pedido no puede ser negativo.";
                    return Presult;
                }

                _logger.LogInformation("EliminarAsync {Id}", removePedido.Id);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_EliminarPedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", removePedido.Id);

                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();

                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo eliminar el pedido.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Pedido eliminado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pedido", ex);
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
                Presult.IsSuccess = false;
                Presult.Message = "Error al obtener el pedido por Id.";
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
                Presult.IsSuccess = false;
                Presult.Message = "Error al obtener todos los pedidos.";
            }

            return Presult;
        }
    }
}

