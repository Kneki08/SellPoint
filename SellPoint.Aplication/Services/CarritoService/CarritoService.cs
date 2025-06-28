using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Services.CarritoService
{
    public sealed class CarritoService : ICarritoService
    {
        private readonly ICategoriaRepository _repository;
        public CarritoService(ICarritoRepository CarritoRepository)
        {
            CarritoRepository = _repository;
        }
        public Task<OperationResult> ActualizarAsync(UpdateCarritoDTO updateCarrito)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(SaveCarritoDTO saveCarrito)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(RemoveCarritoDTO EliminarCarrito)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int ObtenerCarritoDTO)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
