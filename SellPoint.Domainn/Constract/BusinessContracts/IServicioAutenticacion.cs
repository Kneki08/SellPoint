using SellPoint.Domainn.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Domain.Constract.BusinessContracts
{
    public interface IServicioAutenticacion
    {
        Task<(bool Exitoso, Usuario Usuario, string Mensaje)> IniciarSesionAsync(string email, string password);
        Task<(bool Exitoso, string Mensaje)> RegistrarUsuarioAsync(RegistroUsuarioDto registroDto);
        Task<bool> CerrarSesionAsync(int usuarioId);
        Task<string> GenerarHashPasswordAsync(string password);
        Task<bool> ValidarPasswordAsync(string password, string hash);
    }
}
