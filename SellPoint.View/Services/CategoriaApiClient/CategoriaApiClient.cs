using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCategoria;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public class CategoriaApiClient : ICategoriaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoriaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<IEnumerable<CategoriaDTO>> ObtenerTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Categoria/ObtenerTodosAsync");
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<CategoriaModelResponse>(_jsonOptions);
                return ConvertToDtoList(wrapper?.data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categorías: {ex.Message}");
                return new List<CategoriaDTO>();
            }
        }

        public async Task<CategoriaDTO?> ObtenerPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Categoria/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var wrapper = await response.Content.ReadFromJsonAsync<CategoriaModelResponseSingle>(_jsonOptions);
                return ConvertToDto(wrapper?.data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener categoría por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearAsync(SaveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Categoria/SaveCategoriaDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(UpdateCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Categoria/UpdateCategoriaDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarAsync(RemoveCategoriaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Categoria/RemoveCategoriaDTO", dto, _jsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar categoría: {ex.Message}");
                return false;
            }
        }

        private IEnumerable<CategoriaDTO> ConvertToDtoList(IEnumerable<CategoriaModel>? models)
        {
            if (models == null) return new List<CategoriaDTO>();

            var result = new List<CategoriaDTO>();
            foreach (var item in models)
            {
                result.Add(new CategoriaDTO
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    Activo = item.Activo
                });
            }
            return result;
        }

        private CategoriaDTO? ConvertToDto(CategoriaModel? model)
        {
            if (model == null) return null;

            return new CategoriaDTO
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
            };
        }
    }
}

