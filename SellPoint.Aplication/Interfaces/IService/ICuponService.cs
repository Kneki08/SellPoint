using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Interfaces.IService
{
    public interface ICuponService
    {
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> ObtenerPorIdAsync(int cuponId);
        Task<OperationResult> CrearAsync(SaveCuponDTO nuevoCupon);
        Task<OperationResult> ActualizarAsync(UpdateCuponDTO cuponActualizado);
        Task<OperationResult> EliminarAsync(RemoveCuponDTIO cuponAEliminar);
    }
}
