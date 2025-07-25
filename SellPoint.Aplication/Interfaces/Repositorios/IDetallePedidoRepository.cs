
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IDetallepedidoRepository
    {
      Task<OperationResult> GetByIdAsync(int id);
      Task<List<DetallePedidoDTO>> GetAllAsync();
      Task<OperationResult> AddAsync(DetallePedido entity);
      Task<OperationResult> UpdateAsync(DetallePedido entity);
      Task<OperationResult> DeleteAsync(int id);
      Task<OperationResult> SaveChangesAsync();
    }

}
