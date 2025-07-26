using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System.Data;


namespace SellPoint.Persistence.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CategoriaRepository> _logger;

        public CategoriaRepository(string connectionString, ILogger<CategoriaRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        private OperationResult ValidateCategoriaBase(object entidad, bool validarId = false, int? id = null)
        {
            if (entidad is null)
                return OperationResult.Failure("La entidad no puede ser nula.");

            if (validarId && id.HasValue && int.IsNegative(id.Value))
                return OperationResult.Failure("El Id de la categoría no puede ser negativo.");

            return OperationResult.Success();
        }

        private OperationResult ValidateNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return OperationResult.Failure("El nombre de la categoría no puede estar vacío.");

            return OperationResult.Success();
        }

        public async Task<OperationResult> AgregarAsync(SaveCategoriaDTO saveCategoria)
        {
            var validacionBase = ValidateCategoriaBase(saveCategoria);
            if (!validacionBase.IsSuccess) return validacionBase;

            var validacionNombre = ValidateNombre(saveCategoria.Nombre);
            if (!validacionNombre.IsSuccess) return validacionNombre;

            OperationResult result = OperationResult.Success();

            try
            {
                _logger.LogInformation("AgregarAsync {Nombre}", saveCategoria.Nombre);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_AgregarCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", saveCategoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", saveCategoria.Descripcion ?? (object)DBNull.Value);
                await connection.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                result.IsSuccess = rowsAffected > 0;
                result.Message = rowsAffected > 0
                    ? "Categoría agregada correctamente."
                    : "No se pudo agregar la categoría.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la categoría", ex);
            }

            return result;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO updateCategoria)
        {
            // Validaciones de entrada
            if (updateCategoria is null)
                return OperationResult.Failure("La entidad categoría no puede ser nula.");

            if (updateCategoria.Id <= 0)
                return OperationResult.Failure("El ID debe ser mayor que cero.");

            var validacionBase = ValidateCategoriaBase(updateCategoria, true, updateCategoria.Id);
            if (!validacionBase.IsSuccess)
                return validacionBase;

            var validacionNombre = ValidateNombre(updateCategoria.Nombre);
            if (!validacionNombre.IsSuccess)
                return validacionNombre;

            try
            {
                _logger.LogInformation("Iniciando actualización de categoría. ID: {Id}, Nombre: {Nombre}",
                    updateCategoria.Id, updateCategoria.Nombre);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ActualizarCategoria", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", updateCategoria.Id);
                command.Parameters.AddWithValue("@Nombre", updateCategoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", updateCategoria.Descripcion ?? (object)DBNull.Value);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("No se actualizó ninguna categoría. ID posiblemente inexistente: {Id}", updateCategoria.Id);
                    return OperationResult.Failure("No se encontró ninguna categoría con el ID proporcionado para actualizar.");
                }

                _logger.LogInformation("Categoría actualizada correctamente. ID: {Id}", updateCategoria.Id);
                return OperationResult.Success("Categoría actualizada correctamente.");
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Error de SQL al actualizar la categoría. ID: {Id}", updateCategoria.Id);
                return OperationResult.Failure($"Error de base de datos al actualizar la categoría: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general al actualizar la categoría. ID: {Id}", updateCategoria.Id);
                return OperationResult.Failure($"Error inesperado al actualizar la categoría: {ex.Message}");
            }
        }




        public async Task<OperationResult> EliminarAsync(RemoveCategoriaDTO removeCategoria)
        {
            var validacion = ValidateCategoriaBase(removeCategoria, true, removeCategoria?.Id);
            if (!validacion.IsSuccess)
                return validacion;

            try
            {
                _logger.LogInformation("Intentando eliminar categoría con ID: {Id}", removeCategoria.Id);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_EliminarCategoria", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", removeCategoria.Id);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    var mensajeExito = $"Categoría eliminada correctamente. ID: {removeCategoria.Id}";
                    _logger.LogInformation(mensajeExito);
                    return OperationResult.Success(mensajeExito);
                }
                else
                {
                    var mensajeFallo = $"No se encontró ninguna categoría con el ID {removeCategoria.Id} para eliminar. Verifique si ya fue eliminada o si el ID es correcto.";
                    _logger.LogWarning(mensajeFallo);
                    return OperationResult.Failure(mensajeFallo);
                }

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Error de SQL al eliminar la categoría. ID: {Id}", removeCategoria.Id);
                return OperationResult.Failure($"Error de base de datos al eliminar la categoría: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar la categoría. ID: {Id}", removeCategoria.Id);
                return OperationResult.Failure($"Error inesperado al eliminar la categoría: {ex.Message}");
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            if (int.IsNegative(id))
                return OperationResult.Failure("El Id no puede ser negativo.");

            OperationResult result = OperationResult.Success();

            try
            {
                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerCategoriaPorId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var categoria = new CategoriaDTO
                    {
                        Id = reader.GetInt32("Id"),
                        Nombre = reader.GetString("Nombre"),
                        Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                        Activo = reader.GetBoolean("Activo")
                    };

                    result.Data = categoria;
                    result.IsSuccess = true;
                    result.Message = "Categoría obtenida correctamente.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Categoría no encontrada.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la categoría por Id", ex);
            }

            return result;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult result = OperationResult.Success();

            try
            {
                _logger.LogInformation("ObtenerTodosAsync ejecutado");
                var categorias = new List<CategoriaDTO>();

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerTodasCategorias", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    categorias.Add(new CategoriaDTO
                    {
                        Id = reader.GetInt32("Id"),
                        Nombre = reader.GetString("Nombre"),
                        Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                        Activo = reader.GetBoolean("Activo")
                    });
                }

                result.Data = categorias;
                result.IsSuccess = true;
                result.Message = "Categorías obtenidas correctamente.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las categorías", ex);
            }

            return result;
        }
    }
}
