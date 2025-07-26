using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Domain.Base;
using System.Data;

namespace SellPoint.Persistence.Base
{
    public abstract class BaseRepository<T>
    {
        protected readonly string _connectionString;
        protected readonly ILogger<T> _logger;

        protected BaseRepository(string connectionString, ILogger<T> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        protected async Task<OperationResult> ExecuteNonQueryAsync(string storedProcedure, Action<SqlCommand> configureCommand)
        {
            var result = OperationResult.Success();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                configureCommand(command);

                await connection.OpenAsync();
                var affectedRows = await command.ExecuteNonQueryAsync();

                result.IsSuccess = affectedRows > 0;
                result.Message = affectedRows > 0
                    ? "Operación realizada correctamente."
                    : "No se afectaron registros.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al ejecutar {storedProcedure}: {ex.Message}";
                _logger.LogError(ex, "Error en {StoredProcedure}", storedProcedure);
            }

            return result;
        }

        protected async Task<OperationResult> ExecuteReaderAsync<TResult>(
            string storedProcedure,
            Action<SqlCommand> configureCommand,
            Func<SqlDataReader, TResult> map)
        {
            var result = OperationResult.Success();
            try
            {
                var items = new List<TResult>();

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                configureCommand(command);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    items.Add(map(reader));
                }

                result.IsSuccess = true;
                result.Data = items;
                result.Message = "Consulta ejecutada correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al ejecutar {storedProcedure}: {ex.Message}";
                _logger.LogError(ex, "Error en {StoredProcedure}", storedProcedure);
            }

            return result;
        }
    }
}