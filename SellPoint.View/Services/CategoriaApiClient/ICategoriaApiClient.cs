using SellPoint.View.Models.ModelsCategoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public interface ICategoriaApiClient
    {
        Task<IEnumerable<CategoriaDTO>> ObtenerTodosAsync();
        Task<CategoriaDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCategoriaDTO dto);
        Task<bool> ActualizarAsync(UpdateCategoriaDTO dto);
        Task<bool> EliminarAsync(RemoveCategoriaDTO dto);
    }
}
