using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Models.ModelsCupon;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Services.CuponApiClient
{
    public class CuponApiClient : ICuponApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CuponApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<IEnumerable<CuponDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Cupon/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<CuponModelResponse>(_jsonOptions);
                return ConvertToDtoList(wrapper?.data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cupones: {ex.Message}");
                return new List<CuponDTO>();
            }
        }

        public async Task<CuponDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Cupon/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var wrapper = await response.Content.ReadFromJsonAsync<CuponModelResponseSingle>(_jsonOptions);
                return ConvertToDto(wrapper?.data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cupón por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearAsync(SaveCuponDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Cupon/SaveCuponDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cupón: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(UpdateCuponDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Cupon/UpdateCuponDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cupón: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCuponDTIO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Cupon/RemoveCuponDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cupón: {ex.Message}");
                return false;
            }
        }

        private IEnumerable<CuponDTO> ConvertToDtoList(IEnumerable<CuponModel>? models)
        {
            if (models == null) return new List<CuponDTO>();

            var result = new List<CuponDTO>();
            foreach (var item in models)
            {
                result.Add(new CuponDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    ValorDescuento = item.ValorDescuento,
                    FechaVencimiento = item.FechaVencimiento
                });
            }
            return result;
        }

        private CuponDTO? ConvertToDto(CuponModel? model)
        {
            if (model == null) return null;

            return new CuponDTO
            {
                Id = model.Id,
                Codigo = model.Codigo,
                ValorDescuento = model.ValorDescuento,
                FechaVencimiento = model.FechaVencimiento
            };
        }
    }
}
