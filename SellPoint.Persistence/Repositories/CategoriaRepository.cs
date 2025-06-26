using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;


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

        public async Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO updateCategoria)
        {
            OperationResult Presult = OperationResult.Success();
            try 
            {
                if (updateCategoria is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (int.IsNegative(updateCategoria.Id))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id de la categoría no puede ser negativo.";
                    return Presult;
                }

                if (string.IsNullOrWhiteSpace(updateCategoria.Nombre))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El nombre de la categoría no puede estar vacío.";
                    return Presult;
                }

                _logger.LogInformation("ActualizarAsync {Id}", updateCategoria.Id);
                using (var context = new SqlConnection(_connectionString)) 
                {
                    using (var command = new SqlCommand("sp_ActualizarCategoria", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", updateCategoria.Id);
                        command.Parameters.AddWithValue("@Nombre", updateCategoria.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", updateCategoria.Descripcion ?? (object)DBNull.Value);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo actualizar la categoría.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Categoría actualizada correctamente.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la categoría", ex);
            }

            return Presult;
        }

        public async Task<OperationResult> AgregarAsync(SaveCategoriaDTO saveCategoria)
        {
            OperationResult Presult = OperationResult.Success();
            try 
            {
                if (saveCategoria is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (string.IsNullOrWhiteSpace(saveCategoria.Nombre))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El nombre de la categoría no puede estar vacío.";
                    return Presult;
                }

                _logger.LogInformation("AgregarAsync {Nombre}", saveCategoria.Nombre);
                using (var context = new SqlConnection(_connectionString)) 
                {
                    using (var command = new SqlCommand("sp_AgregarCategoria", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", saveCategoria.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", saveCategoria.Descripcion ?? (object)DBNull.Value);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo agregar la categoría.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Categoría agregada correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la categoría", ex);
            }

            return Presult;

        }

        public async Task<OperationResult> EliminarAsync(RemoveCategoriaDTO removeCategoria)
        {
            OperationResult Presult = OperationResult.Success();

            try
            {
                if (removeCategoria is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }

                if (int.IsNegative(removeCategoria.Id))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id de la categoría no puede ser negativo.";
                    return Presult;
                }

                _logger.LogInformation("EliminarAsync {Id}", removeCategoria.Id);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_EliminarCategoria", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", removeCategoria.Id);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo eliminar la categoría.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Categoría eliminada correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría", ex);
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
