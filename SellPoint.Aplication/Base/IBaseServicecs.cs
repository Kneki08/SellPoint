
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Base
{
    public interface IBaseServicecs<TDtoSave, TDtoUpadete, TDtoRemove>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int Id);
        Task<OperationResult> Update(TDtoUpadete dto);
        Task<OperationResult> Remove(TDtoRemove dto);
        Task<OperationResult> Save(TDtoSave dto);
    }
}
