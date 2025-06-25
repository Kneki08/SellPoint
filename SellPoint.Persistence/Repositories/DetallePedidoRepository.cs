using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


namespace SellPoint.Persistence.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DetallePedidoRepository> _logger;
        public DetallePedidoRepository(string connectionString, ILogger<DetallePedidoRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<OperationResult> ActualizarAsync(UpdateDetallePedidoDTO updateDetallePedido)
        {
            OperationResult Presult = new OperationResult();
            try
            {
              _logger.LogInformation("ActualizarAsync {Id}", updateDetallePedido.Id);
                if (updateDetallePedido is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if (updateDetallePedido.Id <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id debe ser mayor que cero.";
                    return Presult;
                }
                if (updateDetallePedido.PedidoId <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El PedidoId debe ser mayor que cero.";
                    return Presult;
                }
                if (updateDetallePedido.ProductoId <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El ProductoId debe ser mayor que cero.";
                    return Presult;
                }
                if (updateDetallePedido.Cantidad <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La Cantidad debe ser mayor que cero.";
                    return Presult;
                }
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ActualizarDetallePedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", updateDetallePedido.Id);
                        command.Parameters.AddWithValue("@PedidoId", updateDetallePedido.PedidoId);
                        command.Parameters.AddWithValue("@ProductoId", updateDetallePedido.ProductoId);
                        command.Parameters.AddWithValue("@Cantidad", updateDetallePedido.Cantidad);
                        
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Detalle del pedido actualizado correctamente.";
                        }
                        else
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo actualizar el detalle del pedido.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el detalle del pedido");
                Presult.IsSuccess = false;
                Presult.Message = "Error al actualizar el detalle del pedido";
            }
            return Presult;
        }
        public async Task<OperationResult> AgregarAsync(SaveDetallePedidoDTO saveDetallePedido)
        {
            OperationResult Presult = new OperationResult();
            try
            {
                _logger.LogInformation("AgregarAsync PedidoId: {PedidoId}, ProductoId: {ProductoId}", saveDetallePedido.PedidoId, saveDetallePedido.ProductoId);

                if (saveDetallePedido is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if (saveDetallePedido.PedidoId <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El PedidoId debe ser mayor que cero.";
                    return Presult;
                }
                if (saveDetallePedido.ProductoId <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El ProductoId debe ser mayor que cero.";
                    return Presult;
                }
                if (saveDetallePedido.Cantidad <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La Cantidad debe ser mayor que cero.";
                    return Presult;
                }

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_AgregarDetallePedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PedidoId", saveDetallePedido.PedidoId);
                        command.Parameters.AddWithValue("@ProductoId", saveDetallePedido.ProductoId);
                        command.Parameters.AddWithValue("@Cantidad", saveDetallePedido.Cantidad);

                        await context.OpenAsync();
                        var resultDB = await command.ExecuteNonQueryAsync();

                        if(resultDB > 0)
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Detalle del pedido agregado correctamente.";
                        }
                        else
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo agregar el detalle del pedido.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el detalle del pedido");
                Presult.IsSuccess = false;
                Presult.Message = "Error al agregar el detalle del pedido.";
            }

            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveDetallePedidoDTO removeDetallePedido)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("EliminarAsync Id: {Id}", removeDetallePedido.Id);

                if (removeDetallePedido is null)
                {
                    result.IsSuccess = false;
                    result.Message = "La entidad no puede ser nula.";
                    return result;
                }

                if (removeDetallePedido.Id <= 0)
                {
                    result.IsSuccess = false;
                    result.Message = "El Id debe ser mayor que cero.";
                    return result;
                }

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_EliminarDetallePedido", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", removeDetallePedido.Id);

                        await context.OpenAsync();
                        var resultDB = await command.ExecuteNonQueryAsync();

                        if(resultDB > 0)
                        {
                            result.IsSuccess = true;
                            result.Message = "Detalle del pedido eliminado correctamente.";
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "No se pudo eliminar el detalle del pedido.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el detalle del pedido");
                result.IsSuccess = false;
                result.Message = "Error al eliminar el detalle del pedido.";
            }

            return result;
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
