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
        public async Task<OperationResult> ActualizarAsync(UpdateCuponDTO updateCupon)
        {
         OperationResult Presult = new OperationResult();
            try
            {
                if (updateCupon is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if (updateCupon.Id <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El Id debe ser mayor que cero.";
                    return Presult;
                }
                if (string.IsNullOrWhiteSpace(updateCupon.Codigo))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El código del cupón no puede estar vacío.";
                    return Presult;
                }
                if (updateCupon.ValorDescuento <= 0)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El valor del descuento debe ser mayor que cero.";
                    return Presult;
                }
                if (updateCupon.FechaVencimiento <= DateTime.Now)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La fecha de vencimiento debe ser una fecha futura.";
                    return Presult;
                }

                _logger.LogInformation("ActualizarAsync {Id}", updateCupon.Id);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ActualizarCupon", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", updateCupon.Id);
                        command.Parameters.AddWithValue("@Codigo", updateCupon.Codigo);
                        command.Parameters.AddWithValue("@Descuento", updateCupon.ValorDescuento);
                        command.Parameters.AddWithValue("@FechaExpiracion", updateCupon.FechaVencimiento);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo actualizar el cupón.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Cupón actualizado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cupón");
                Presult.IsSuccess = false;
                Presult.Message = "Error al actualizar el cupón: " + ex.Message;
            }
            return Presult;
        }

        public async Task<OperationResult> AgregarAsync(SaveCuponDTO saveCupon)
        {
            OperationResult Presult = new OperationResult();

            if (saveCupon is null)
            {
                Presult.IsSuccess = false;
                Presult.Message = "La entidad no puede ser nula.";
                return Presult;
            }
            if (string.IsNullOrWhiteSpace(saveCupon.Codigo))
            {
                Presult.IsSuccess = false;
                Presult.Message = "El código del cupón no puede estar vacío.";
                return Presult;
            }
            if (saveCupon.ValorDescuento <= 0)
            {
                Presult.IsSuccess = false;
                Presult.Message = "El valor del descuento debe ser mayor que cero.";
                return Presult;
            }
            if (saveCupon.FechaVencimiento <= DateTime.Now)
            {
                Presult.IsSuccess = false;
                Presult.Message = "La fecha de vencimiento debe ser una fecha futura.";
                return Presult;
            }

            try
            {
                _logger.LogInformation("AgregarAsync {Codigo}", saveCupon.Codigo);
                using(var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_AgregarCupon", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Codigo", saveCupon.Codigo);
                        command.Parameters.AddWithValue("@Descuento", saveCupon.ValorDescuento);
                        command.Parameters.AddWithValue("@FechaExpiracion", saveCupon.FechaVencimiento);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo agregar el cupón.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Cupón agregado correctamente.";
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cupón");
                Presult.IsSuccess = false;
                Presult.Message = "Error al agregar el cupón: " + ex.Message;
            }
            return Presult;

        }

        public async Task<OperationResult> EliminarAsync(RemoveCuponDTIO removeCupon)
        {
            OperationResult Presult = new OperationResult();
            
            if (removeCupon is null)
            {
                Presult.IsSuccess = false;
                Presult.Message = "La entidad no puede ser nula.";
                return Presult;
            }
            if (removeCupon.Id <= 0)
            {
                Presult.IsSuccess = false;
                Presult.Message = "El Id debe ser mayor que cero.";
                return Presult;
            }
            try
            {
                _logger.LogInformation("EliminarAsync {Id}", removeCupon.Id);
                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_EliminarCupon", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", removeCupon.Id);
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();
                        if (result <= 0)
                        {
                            Presult.IsSuccess = false;
                            Presult.Message = "No se pudo eliminar el cupón.";
                        }
                        else
                        {
                            Presult.IsSuccess = true;
                            Presult.Message = "Cupón eliminado correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cupón");
                Presult.IsSuccess = false;
                Presult.Message = "Error al eliminar el cupón: " + ex.Message;
            }
            return Presult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            OperationResult Presult = new OperationResult();

            if (id <= 0)
            {
                Presult.IsSuccess = false;
                Presult.Message = "El Id debe ser mayor que cero.";
                return Presult;
            }

            try
            {
                _logger.LogInformation("ObtenerPorIdAsync {Id}", id);
                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerCuponPorId", context);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var cupon = new CuponDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                        ValorDescuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                        FechaVencimiento = reader.GetDateTime(reader.GetOrdinal("FechaExpiracion"))
                    };

                    Presult.IsSuccess = true;
                    Presult.Message = "Cupón obtenido correctamente.";
                    Presult.Data = cupon;
                }
                else
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "Cupón no encontrado.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cupón por ID");
                Presult.IsSuccess = false;
                Presult.Message = "Error al obtener el cupón por ID: " + ex.Message;
            }

            return Presult;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult Presult = new OperationResult();
            var cupones = new List<CuponDTO>();

            try
            {
                _logger.LogInformation("ObtenerTodosAsync ejecutado");
                using var context = new SqlConnection(_connectionString);
                using var command = new SqlCommand("sp_ObtenerTodosCupones", context);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                await context.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var cupon = new CuponDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                        ValorDescuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                        FechaVencimiento = reader.GetDateTime(reader.GetOrdinal("FechaExpiracion"))
                    };
                    cupones.Add(cupon);
                }

                Presult.IsSuccess = true;
                Presult.Message = "Cupones obtenidos correctamente.";
                Presult.Data = cupones;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los cupones");
                Presult.IsSuccess = false;
                Presult.Message = "Error al obtener todos los cupones: " + ex.Message;
            }

            return Presult;
        }
    }
}
