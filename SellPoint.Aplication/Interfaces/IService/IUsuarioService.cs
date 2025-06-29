using SellPoint.Aplication.Dtos.Cliente;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SellPoint.Aplication.Interfaces.IService
{
    public interface IUsuarioService
    {
        Task<OperationResult> ObtenerPorIdAsync(int id);
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> AgregarAsync(SaveClienteDTO saveCliente);
        Task<OperationResult> ActualizarAsync(UpdateClienteDTO updateCliente);
        Task<OperationResult> EliminarAsync(RemoveClienteDTO removeCliente);

    }
}
