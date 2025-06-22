using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Cliente
{
    public record SaveClienteDTO
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }
        public string Email { get; init; }
        public string Telefono { get; init; }
        public DateTime? FechaNacimiento { get; init; }
        public string PasswordHash { get; init; }
        public bool Activo { get; init; }
    }
}
