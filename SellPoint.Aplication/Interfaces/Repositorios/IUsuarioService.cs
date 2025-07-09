using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cliente;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface IUsuarioRepository 
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveClienteDTO saveClienteDTO);
        Task<OperationResult> ActualizarAsync(UpdateClienteDTO updateClienteDTO);
        Task<OperationResult> EliminarAsync(RemoveClienteDTO removeClienteDTO);
    }

}
