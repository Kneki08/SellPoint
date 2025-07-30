using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
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

                command.Parameters.AddWithValue("@codigo", saveCupon.Codigo);
                command.Parameters.AddWithValue("@descripcion", (object?)saveCupon.Descripcion ?? DBNull.Value);
                command.Parameters.AddWithValue("@tipo_descuento", (object?)saveCupon.TipoDescuento ?? DBNull.Value);
                command.Parameters.AddWithValue("@valor_descuento", saveCupon.ValorDescuento);
                command.Parameters.AddWithValue("@monto_minimo", saveCupon.MontoMinimo);
                command.Parameters.AddWithValue("@fecha_inicio", saveCupon.FechaInicio);
                command.Parameters.AddWithValue("@fecha_vencimiento", saveCupon.FechaVencimiento);
                command.Parameters.AddWithValue("@usos_maximos", (object?)saveCupon.UsosMaximos ?? DBNull.Value);
                command.Parameters.AddWithValue("@usos_actuales", 0); // inicial
                command.Parameters.AddWithValue("@activo", saveCupon.Activo);
                command.Parameters.AddWithValue("@fecha_creacion", DateTime.Now);

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
            if (updateCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (updateCupon.Id <= 0)
                return OperationResult.Failure("El ID del cupón debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(updateCupon.Codigo))
                return OperationResult.Failure("El código del cupón no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(updateCupon.TipoDescuento))
                return OperationResult.Failure("El tipo de descuento es obligatorio.");

            if (updateCupon.ValorDescuento <= 0)
                return OperationResult.Failure("El valor del descuento debe ser mayor que cero.");

            if (updateCupon.MontoMinimo < 0)
                return OperationResult.Failure("El monto mínimo no puede ser negativo.");

            if (updateCupon.FechaVencimiento <= updateCupon.FechaInicio)
                return OperationResult.Failure("La fecha de vencimiento debe ser posterior a la fecha de inicio.");

            try
            {
                _logger.LogInformation("Iniciando actualización del cupón con ID: {Id}", updateCupon.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarCupon", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", updateCupon.Id);
                command.Parameters.AddWithValue("@Codigo", updateCupon.Codigo);
                command.Parameters.AddWithValue("@Descripcion", updateCupon.Descripcion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TipoDescuento", updateCupon.TipoDescuento);
                command.Parameters.AddWithValue("@ValorDescuento", updateCupon.ValorDescuento);
                command.Parameters.AddWithValue("@MontoMinimo", updateCupon.MontoMinimo);
                command.Parameters.AddWithValue("@FechaInicio", updateCupon.FechaInicio);
                command.Parameters.AddWithValue("@FechaVencimiento", updateCupon.FechaVencimiento);
                command.Parameters.AddWithValue("@UsosMaximos", updateCupon.UsosMaximos ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Activo", updateCupon.Activo);

                await connection.OpenAsync();
                var rows = await command.ExecuteNonQueryAsync();

                if (rows > 0)
                {
                    _logger.LogInformation("Cupón actualizado correctamente. ID: {Id}", updateCupon.Id);
                    return OperationResult.Success("Cupón actualizado correctamente.");
                }
                else
                {
                    _logger.LogWarning("No se encontró un cupón con el ID proporcionado: {Id}", updateCupon.Id);
                    return OperationResult.Failure("No se encontró ningún cupón con el ID proporcionado para actualizar.");
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error SQL al actualizar el cupón. ID: {Id}", updateCupon.Id);
                return OperationResult.Failure($"Error SQL al actualizar el cupón: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general al actualizar el cupón. ID: {Id}", updateCupon.Id);
                return OperationResult.Failure($"Error inesperado al actualizar el cupón: {ex.Message}");
            }
        }


        public async Task<OperationResult> EliminarAsync(RemoveCuponDTIO removeCupon)
        {
            if (removeCupon is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (removeCupon.Id <= 0)
                return OperationResult.Failure("El ID del cupón debe ser mayor que cero.");

            try
            {
                _logger.LogInformation("Intentando eliminar cupón con ID: {Id}", removeCupon.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarCupon", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", removeCupon.Id);

                await connection.OpenAsync();
                var rows = await command.ExecuteNonQueryAsync();

                if (rows > 0)
                {
                    _logger.LogInformation("Cupón eliminado correctamente. ID: {Id}", removeCupon.Id);
                    return OperationResult.Success("Cupón eliminado correctamente.");
                }
                else
                {
                    _logger.LogWarning("No se encontró ningún cupón con el ID proporcionado: {Id}", removeCupon.Id);
                    return OperationResult.Failure("No se encontró ningún cupón con el ID proporcionado para eliminar.");
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error SQL al eliminar el cupón. ID: {Id}", removeCupon.Id);
                return OperationResult.Failure($"Error SQL al eliminar el cupón: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general al eliminar el cupón. ID: {Id}", removeCupon.Id);
                return OperationResult.Failure($"Error inesperado al eliminar el cupón: {ex.Message}");
            }
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