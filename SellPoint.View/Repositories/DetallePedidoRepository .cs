using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.View.Models.ModelDetallePedido;
using SellPoint.View.Service.DetallePedidoClient.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SellPoint.View.Validations;

namespace SellPoint.View.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly IDetallePedidoApiClient _apiClient;
       

        public DetallePedidoRepository(IDetallePedidoApiClient apiClient)
        {
            _apiClient = apiClient;
            
        }

        public async Task<ApiResponse<IEnumerable<DetallePedidoDTO>>> GetAllAsync()
        {
            try
            {
                var response = await _apiClient.ObtenerTodosAsync();

                if (!response.Success)
                    return ApiResponse<IEnumerable<DetallePedidoDTO>>.CreateError(response.Message);

                // Mapeo manual
                var mappedData = response.Data?.Select(model => new DetallePedidoDTO
                {
                    Id = model.Id,
                    Cantidad = model.Cantidad,
                    PrecioUnitario = model.PrecioUnitario,
                    PedidoId = model.PedidoId,
                    ProductoId = model.ProductoId,
                  
                });

                return ApiResponse<IEnumerable<DetallePedidoDTO>>.CreateSuccess(mappedData);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<DetallePedidoDTO>>.CreateError($"Error al obtener los detalles: {ex.Message}");
            }
        }

        public async Task<ApiResponse<DetallePedidoDTO>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return ApiResponse<DetallePedidoDTO>.CreateError("ID inválido");

                var response = await _apiClient.ObtenerPorIdAsync(id);

                if (!response.Success)
                    return ApiResponse<DetallePedidoDTO>.CreateError(response.Message);

                // Mapeo manual
                var mappedData = new DetallePedidoDTO
                {
                    Id = response.Data.Id,
                    Cantidad = response.Data.Cantidad,
                    PrecioUnitario = response.Data.PrecioUnitario,
                    PedidoId = response.Data.PedidoId,
                    ProductoId = response.Data.ProductoId,
                    
                };

                return ApiResponse<DetallePedidoDTO>.CreateSuccess(mappedData);
            }
            catch (Exception ex)
            {
                return ApiResponse<DetallePedidoDTO>.CreateError($"Error al obtener el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> CreateAsync(SaveDetallePedidoDTO dto)
        {
            try
            {
                // Validación del DTO
                DetallePedidoValidator.Validate(dto);

                var response = await _apiClient.CrearAsync(dto);

                return response.Success
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle creado exitosamente")
                    : ApiResponse<bool>.CreateError(response.Message);
            }
            catch (ValidationException vex)
            {
                return ApiResponse<bool>.CreateError(vex.ValidationResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error al crear el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> UpdateAsync(UpdateDetallePedidoDTO dto)
        {
            try
            {
                // Validación del DTO
                DetallePedidoValidator.Validate(dto);

                var response = await _apiClient.ActualizarAsync(dto);

                return response.Success
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle actualizado exitosamente")
                    : ApiResponse<bool>.CreateError(response.Message);
            }
            catch (ValidationException vex)
            {
                return ApiResponse<bool>.CreateError(vex.ValidationResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error al actualizar el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return ApiResponse<bool>.CreateError("ID inválido");

                var dto = new RemoveDetallePedidoDTO { Id = id };
                var response = await _apiClient.EliminarAsync(dto);

                return response.Success
                    ? ApiResponse<bool>.CreateSuccess(true, "Detalle eliminado exitosamente")
                    : ApiResponse<bool>.CreateError(response.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.CreateError($"Error al eliminar el detalle: {ex.Message}");
            }
        }

        public async Task<ApiResponse<DetallePedidoDTO>> GetByPedidoIdAsync(int pedidoId)
        {
            try
            {
                if (pedidoId <= 0)
                    return ApiResponse<DetallePedidoDTO>.CreateError("Pedido ID inválido");

                var allResponse = await _apiClient.ObtenerTodosAsync();

                if (!allResponse.Success)
                    return ApiResponse<DetallePedidoDTO>.CreateError(allResponse.Message);

                var detalle = allResponse.Data?.FirstOrDefault(d => d.PedidoId == pedidoId);

                if (detalle == null)
                    return ApiResponse<DetallePedidoDTO>.CreateError("No se encontró detalle para el pedido especificado");

                // Mapeo manual
                var mappedData = new DetallePedidoDTO
                {
                    Id = detalle.Id,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    PedidoId = detalle.PedidoId,
                    ProductoId = detalle.ProductoId,
                    
                };

                return ApiResponse<DetallePedidoDTO>.CreateSuccess(mappedData);
            }
            catch (Exception ex)
            {
                return ApiResponse<DetallePedidoDTO>.CreateError($"Error al buscar por Pedido ID: {ex.Message}");
            }
        }
    }
}
