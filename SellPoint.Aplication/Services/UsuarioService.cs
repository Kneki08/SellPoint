using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Cliente;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Users;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Persistence.Context;

namespace SellPoint.Aplication.Services.UsuarioService
{
    public sealed class UsuarioService : IUsuarioService
    {
        private readonly DbContext _context;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(DbContext context, ILogger<UsuarioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> AgregarAsync(SaveClienteDTO savecliente)
        {
            if (savecliente == null)
                return OperationResult.Failure("El DTO no puede ser nulo.");

            try
            {
                var usuario = new Cliente
                {
                    Nombre = savecliente.Nombre,
                    Apellido = savecliente.Apellido,
                    Email = savecliente.Email,
                    Telefono = savecliente.Telefono,
                    //FechaNacimiento = savecliente.FechaNacimiento,
                    
                };

                await _context.clientes.AddAsync(usuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cliente agregado correctamente: {Nombre} {Apellido}", savecliente.Nombre, savecliente.Apellido);
                return OperationResult.Success("Cliente agregado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cliente");
                return OperationResult.Failure("Error al agregar el cliente");
            }
        }

        public async Task<OperationResult> EliminarAsync(RemoveClienteDTO removeCliente)
        {
            try
            {
                var usuario = await _context.clientes.FindAsync(removeCliente.Id);
                if (usuario == null)
                    return OperationResult.Failure("Cliente no encontrado.");

                _context.clientes.Remove(usuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cliente eliminado: {Id}", removeCliente.Id);
                return OperationResult.Success("Cliente eliminado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente");
                return OperationResult.Failure("Error al eliminar el cliente");
            }
        }

        public async Task<OperationResult> ObtenerTodosAsync()
        {
            try
            {
                var clientes = await _context.clientes.ToListAsync();
                return OperationResult.Success(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes");
                return OperationResult.Failure("Error al obtener los clientes");
            }
        }

        public async Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult.Failure("El Id debe ser mayor que cero.");

            try
            {
                var cliente = await _context.clientes.FindAsync(id);
                if (cliente == null)
                    return OperationResult.Failure("Cliente no encontrado.");

                return OperationResult.Success(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente por ID");
                return OperationResult.Failure("Error al obtener el cliente");
            }
        }

        public async Task<OperationResult> ActualizarAsync(UpdateClienteDTO updatecliente)
        {
            try
            {
                var usuario = await _context.clientes.FindAsync(updatecliente.Id);
                if (usuario == null)
                    return OperationResult.Failure("Cliente no encontrado.");

                usuario.Email = updatecliente.Email;
                usuario.Telefono = updatecliente.Telefono;
                usuario.Activo = updatecliente.Activo;

                _context.clientes.Update(usuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cliente actualizado: {Id}", updatecliente.Id);
                return OperationResult.Success("Cliente actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente");
                return OperationResult.Failure("Error al actualizar el cliente");
            }
        }
    }
}

