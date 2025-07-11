﻿
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System.Data;


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
               Presult.IsSuccess = false;
                Presult.Message = "Error al agregar el carrito: " + ex.Message;
                _logger.LogError(ex, "Error al agregar el carrito");
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
               Presult.IsSuccess = false;
                Presult.Message = "Error al actualizar el carrito: " + ex.Message;
                _logger.LogError(ex, "Error al actualizar el carrito"); 
            }
            return Presult;
        }

        public async Task<OperationResult> EliminarAsync(RemoveCarritoDTO removeCarrito)
        {
            OperationResult Presult = OperationResult.Success();
            try
            {
                if (removeCarrito is null)
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "La entidad no puede ser nula.";
                    return Presult;
                }
                if (int.IsNegative(removeCarrito.UsuarioId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El UsuarioId no puede ser negativo.";
                    return Presult;
                }
                if (int.IsNegative(removeCarrito.ProductoId))
                {
                    Presult.IsSuccess = false;
                    Presult.Message = "El ProductoId no puede ser negativo.";
                    return Presult;
                }

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
                Presult.IsSuccess = false;
                Presult.Message = "Error al eliminar el carrito: " + ex.Message;
                _logger.LogError(ex, "Error al eliminar el carrito");
            }
            return Presult;
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int ObtenerCarritoDTO)
        {
            OperationResult result = OperationResult.Success();
            try
            {

               List<ObtenerCarritoDTO> items = new List<ObtenerCarritoDTO>();

                using (var context = new SqlConnection(this._connectionString))
                {
                    using (var command = new SqlCommand("sp_ObtenerCarritoPorUsuario", context))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", ObtenerCarritoDTO);
                        await context.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                items.Add(new ObtenerCarritoDTO
                                {
                                    ProductoId = reader.GetInt32("ProductoId"),
                                    Nombre = reader.GetString("Nombre"),
                                    Precio = reader.GetDecimal("Precio"),
                                    Cantidad = reader.GetInt32("Cantidad"),
                                    Subtotal = reader.GetDecimal("Subtotal")
                                });
                            }
                        }
                    }
                }

                result.Data = items;
                result.IsSuccess = true;
                result.Message = "Carrito cargado correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al obtener el carrito: " + ex.Message;
                _logger.LogError(ex, "Error al obtener el carrito por ID: {Id}", ObtenerCarritoDTO);
            }

            return result;
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult result = OperationResult.Success();

            try
            {
                List<ObtenerCarritoDTO> items = new List<ObtenerCarritoDTO>();

                using (var context = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ObtenerTodosLosCarritos", context))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await context.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<ObtenerCarritoDTO> obtenerCarritos = new List<ObtenerCarritoDTO>();
                            while (await reader.ReadAsync())
                            {
                                ObtenerCarritoDTO obtenerCarrito = new ObtenerCarritoDTO
                                {
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
                                };
                            }
                        }
                    }
                }

                result.IsSuccess = true;
                result.Data = items;
                result.Message = "Carritos obtenidos correctamente.";
            }
            catch (Exception ex)
            {
              result.IsSuccess = false;
                result.Message = "Error al obtener los carritos: " + ex.Message;
                _logger.LogError(ex, "Error al obtener todos los carritos");
            }

            return result;
        }
    }
}
