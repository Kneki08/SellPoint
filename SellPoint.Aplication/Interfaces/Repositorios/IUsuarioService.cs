using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Interfaces.Repositorios
{
    public interface  IUsuarioService
    {
        Task<OperationResult<List<UsuarioBase>>> ObtenerUsuariosAsync();
        Task<OperationResult<Usuario>> ObtenerPorEmailAsync(string email);
        Task<OperationResult> CrearUsuarioAsync(Usuario usuario);
        Task<OperationResult> ActualizarUsuarioAsync(Usuario usuario);
        Task<OperationResult> EliminarUsuarioAsync(int id);
    }
}
