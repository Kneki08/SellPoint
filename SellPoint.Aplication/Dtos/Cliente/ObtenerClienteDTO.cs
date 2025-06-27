using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Dtos.Cliente
{
    public record ObtenerClienteDTO
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Apellido { get; init; } = string.Empty;
        public string Correo { get; init; } = string.Empty;
        public string? Telefono { get; init; }
        public bool Activo { get; init; }
    }
}
