using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Cliente;
using SellPoint.Aplication.Interfaces.IService;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Domain.Base;

namespace SellPoint.Aplication.Services.UsuarioService
{
    public sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository UsuarioRepository, ILogger<UsuarioService> logger, IConfiguration Configuration)
        {
            _UsuarioRepository = UsuarioRepository;
            _logger = logger;
            _configuration = Configuration;
        }

        public async Task<OperationResult> AgregarAsync(SaveClienteDTO savecliente)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Agregando el cliente", savecliente);
                if (savecliente is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _UsuarioRepository.AgregarAsync(savecliente);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo agregar el cliente: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Cliente agregado correctamente para Nombre: {Nombre}, Apellido: {Apellido}, Telefono: {Telefono},Email: {Email}, FechaNacimiento: {FechaNacimiento} ",
                    savecliente.Nombre, savecliente.Apellido, savecliente.Telefono, savecliente.Email, savecliente.FechaNacimiento);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cliente");
                operation.IsSuccess = false;
                operation.Message = "Error al agregar el cliente";

            }
            return operation;
        }

        public async Task<OperationResult> EliminarAsync(RemoveClienteDTO removeCliente)
        {
            OperationResult operation = new OperationResult();
            try
            {
                _logger.LogInformation("Eliminando el cliente", removeCliente);
                if (removeCliente is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _UsuarioRepository.EliminarAsync(removeCliente);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo eliminar el cliente: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Cliente eliminado correctamente para Id: {Id}",
                    removeCliente.Id);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente");
                operation.IsSuccess = false;
                operation.Message = "Error al eliminar el cliente";

            }
            return operation;
        }
        public async Task<OperationResult> ObtenerTodosAsync()
        {
            OperationResult operation = new OperationResult();
            try
            {
                operation = await _UsuarioRepository.ObtenerTodosAsync();
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener todos los clientes: {Message}", operation.Message);
                    return operation;
                }
                _logger.LogInformation("Clientes obtenidos correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los clientes {ex.Message}");
                operation = OperationResult.Failure($"Error al obtener todos los clientes {ex.Message}");
            }
            return operation;
        }
        public async Task<OperationResult> ObtenerPorIdAsync(int ObtenerclienteDTO)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Obteniendo el cliente por ID: {Id}", ObtenerclienteDTO);
                if (ObtenerclienteDTO <= 0)
                {
                    _logger.LogError("El ID del pedido debe ser mayor que cero");
                    return operation;
                }
                operation = await _UsuarioRepository.ObtenerPorIdAsync(ObtenerclienteDTO);
                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo obtener el cliente por ID: {Id}, Error: {Message}", ObtenerclienteDTO, operation.Message);
                    return operation;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente por ID");
                operation.IsSuccess = false;
                operation.Message = "Error al obtener el cliente por ID";
            }
            return operation;
        }

        public async Task<OperationResult> ActualizarAsync(UpdateClienteDTO updatecliente)
        {
            OperationResult operation = new OperationResult();

            try
            {
                _logger.LogInformation("Actualizando el cliente", updatecliente);

                if (updatecliente is null)
                {
                    _logger.LogError("Se requiere crear un DTO");
                    return operation;
                }
                operation = await _UsuarioRepository.ActualizarAsync(updatecliente);

                if (!operation.IsSuccess)
                {
                    _logger.LogError("No se pudo actualizar el cliente: {Message}", operation.Message);
                    return operation;
                }

                _logger.LogInformation("Cliente actualizado correctamente para Id: {Id}, Email: {Email}, Telefono: {Telefono}, Activo: {Activo}",
                    updatecliente.Id, updatecliente.Email, updatecliente.Telefono, updatecliente.Activo);
                return operation;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al actualizar el cliente");
                operation.IsSuccess = false;
                operation.Message = "Error al actualizar el cliente";

            }
            return operation;
        }
    }
}
