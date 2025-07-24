using SellPoint.View.Models.ModelsCupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.View.Services.CuponApiClient
{
    public interface ICuponApiClient
    {
        Task<IEnumerable<CuponDTO>> ObtenerTodosAsync();
        Task<CuponDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SaveCuponDTO dto);
        Task<bool> ActualizarAsync(UpdateCuponDTO dto);
        Task<bool> EliminarAsync(RemoveCuponDTO dto);
    }
}
