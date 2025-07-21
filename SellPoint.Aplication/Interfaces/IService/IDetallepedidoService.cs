
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Servicios
{
    public interface IDetallepedidoService  
    {
        Task<OperationResult> AddAsync(SaveDetallePedidoDTO dto);
        Task<OperationResult> UpdateAsync(UpdateDetallePedidoDTO dto);
        Task<OperationResult> DeleteAsync(RemoveDetallePedidoDTO dto);
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> SaveChangesAsync();

    }
}
