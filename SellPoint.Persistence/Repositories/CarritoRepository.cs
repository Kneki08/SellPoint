
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


namespace SellPoint.Persistence.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CarritoRepository> _logger;

        public CarritoRepository(string connectionString, ILogger<CarritoRepository> Logger)
        {
            _connectionString = connectionString;
            _logger = Logger;
        }

        public async Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarrito)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if(saveCarrito is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if(int.IsNegative(saveCarrito.UsuarioId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El UsuarioId no puede ser negativo.";
                    return Presult;
                }
                if(int.IsNegative(saveCarrito.ProductoId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El ProductoId no puede ser negativo.";
                    return Presult;
                }
                if(int.IsNegative(saveCarrito.Cantidad) || saveCarrito.Cantidad == 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La Cantidad debe ser mayor que cero.";
                    return Presult;
                }
                _logger.LogInformation("agregarAsync {UsuarioId}", saveCarrito.UsuarioId);
                using (var context = new SqlConnection(_connectionString))
                {
                   
                    using (var command = new SqlCommand("sp_AgregarCarrito", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", saveCarrito.UsuarioId);
                        command.Parameters.AddWithValue("@ProductoId", saveCarrito.ProductoId);
                        command.Parameters.AddWithValue("@Cantidad", saveCarrito.Cantidad);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "No se pudo agregar el carrito.";
                        }
                        else
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "Carrito agregado correctamente.";
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el carrito", ex);
            }
            return Presult;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarrito)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (updateCarrito is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if (int.IsNegative(updateCarrito.UsuarioId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El UsuarioId no puede ser negativo.";
                    return Presult;
                }
                if (int.IsNegative(updateCarrito.ProductoId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El ProductoId no puede ser negativo.";
                    return Presult;
                }
                if (int.IsNegative(updateCarrito.NuevaCantidad) || updateCarrito.NuevaCantidad == 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La NuevaCantidad debe ser mayor que cero.";
                    return Presult;
                }
                _logger.LogInformation("ActualizarAsync {UsuarioId}", updateCarrito.UsuarioId);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ActualizarCarrito", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", updateCarrito.UsuarioId);
                        command.Parameters.AddWithValue("@ProductoId", updateCarrito.ProductoId);
                        command.Parameters.AddWithValue("@NuevaCantidad", updateCarrito.NuevaCantidad);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo actualizar el carrito.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Carrito actualizado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el carrito", ex);
            }
            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCarritoDTO removeCarrito)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
              _logger.LogInformation("EliminarAsync {UsuarioId}", removeCarrito.UsuarioId);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_EliminarCarrito", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", removeCarrito.UsuarioId);
                        command.Parameters.AddWithValue("@ProductoId", removeCarrito.ProductoId);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo eliminar el carrito.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Carrito eliminado correctamente.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el carrito", ex);
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