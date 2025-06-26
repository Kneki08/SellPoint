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

        public  Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}

