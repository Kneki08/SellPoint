using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System.Data;


namespace SellPoint.Persistence.Repositories
{
    public class CuponRepository : ICuponRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CuponRepository> _logger;

        public CuponRepository(string connectionString, ILogger<CuponRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> AgregarAsync(SaveCuponDTO saveCupon)
        {
            var result = new OperationResult();

            if (saveCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");
            if (string.IsNullOrWhiteSpace(saveCupon.Codigo))
                return OperationResult.Failure("El código del cupón no puede estar vacío.");
            if (saveCupon.ValorDescuento <= 0)
                return OperationResult.Failure("El valor del descuento debe ser mayor que cero.");
            if (saveCupon.FechaVencimiento <= DateTime.Now)
                return OperationResult.Failure("La fecha de vencimiento debe ser una fecha futura.");

            try
            {
                _logger.LogInformation("AgregarAsync {Codigo}", saveCupon.Codigo);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_AgregarCupon", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Codigo", saveCupon.Codigo);
                command.Parameters.AddWithValue("@Descuento", saveCupon.ValorDescuento);
                command.Parameters.AddWithValue("@FechaExpiracion", saveCupon.FechaVencimiento);
                await connection.OpenAsync();

                var rows = await command.ExecuteNonQueryAsync();
                result.IsSuccess = rows > 0;
                result.Message = rows > 0 ? "Cupón agregado correctamente." : "No se pudo agregar el cupón.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cupón");
                result.IsSuccess = false;
                result.Message = $"Error al agregar el cupón: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCuponDTO updateCupon)
        {
            var result = new OperationResult();

            if (updateCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");
            if (updateCupon.Id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");
            if (string.IsNullOrWhiteSpace(updateCupon.Codigo))
                return OperationResult.Failure("El código del cupón no puede estar vacío.");
            if (updateCupon.ValorDescuento <= 0)
                return OperationResult.Failure("El valor del descuento debe ser mayor que cero.");
            if (updateCupon.FechaVencimiento <= DateTime.Now)
                return OperationResult.Failure("La fecha de vencimiento debe ser una fecha futura.");

            try
            {
                _logger.LogInformation("ActualizarAsync {Id}", updateCupon.Id);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarCupon", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", updateCupon.Id);
                command.Parameters.AddWithValue("@Codigo", updateCupon.Codigo);
                command.Parameters.AddWithValue("@Descuento", updateCupon.ValorDescuento);
                command.Parameters.AddWithValue("@FechaExpiracion", updateCupon.FechaVencimiento);
                await connection.OpenAsync();

                var rows = await command.ExecuteNonQueryAsync();
                result.IsSuccess = rows > 0;
                result.Message = rows > 0 ? "Cupón actualizado correctamente." : "No se pudo actualizar el cupón.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cupón");
                result.IsSuccess = false;
                result.Message = $"Error al actualizar el cupón: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCuponDTIO removeCupon)
        {
            var result = new OperationResult();

            if (removeCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");
            if (removeCupon.Id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                _logger.LogInformation("EliminarAsync {Id}", removeCupon.Id);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarCupon", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", removeCupon.Id);
                await connection.OpenAsync();

                var rows = await command.ExecuteNonQueryAsync();
                result.IsSuccess = rows > 0;
                result.Message = rows > 0 ? "Cupón eliminado correctamente." : "No se pudo eliminar el cupón.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cupón");
                result.IsSuccess = false;
                result.Message = $"Error al eliminar el cupón: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            var result = new OperationResult();

            if (id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerCuponPorId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var cupon = new CuponDTO
                    {
                        Id = reader.GetInt32("Id"),
                        Codigo = reader.GetString("Codigo"),
                        ValorDescuento = reader.GetDecimal("Descuento"),
                        FechaVencimiento = reader.GetDateTime("FechaExpiracion")
                    };

                    result.IsSuccess = true;
                    result.Message = "Cupón obtenido correctamente.";
                    result.Data = cupon;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Cupón no encontrado.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cupón por ID");
                result.IsSuccess = false;
                result.Message = $"Error al obtener el cupón por ID: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            var result = new OperationResult();
            var cupones = new List<CuponDTO>();

            try
            {
                _logger.LogInformation("ObtenerTodosAsync ejecutado");
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerTodosCupones", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    cupones.Add(new CuponDTO
                    {
                        Id = reader.GetInt32("Id"),
                        Codigo = reader.GetString("Codigo"),
                        ValorDescuento = reader.GetDecimal("Descuento"),
                        FechaVencimiento = reader.GetDateTime("FechaExpiracion")
                    });
                }

                result.IsSuccess = true;
                result.Message = "Cupones obtenidos correctamente.";
                result.Data = cupones;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los cupones");
                result.IsSuccess = false;
                result.Message = $"Error al obtener todos los cupones: {ex.Message}";
            }

            return result;
        }
    }
}