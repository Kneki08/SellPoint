using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Test.CarritoPersistence
{
    public class CarritoAgregarAsyncTests
    {
        private readonly CarritoRepository _repository;

        public CarritoAgregarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CarritoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CarritoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsNegativo()
        {
            var dto = new SaveCarritoDTO
            {
                UsuarioId = -1,
                ProductoId = 1,
                Cantidad = 1
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El UsuarioId no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoProductoIdEsNegativo()
        {
            var dto = new SaveCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = -1,
                Cantidad = 1
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ProductoId no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoCantidadEsCero()
        {
            var dto = new SaveCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = 1,
                Cantidad = 0
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La Cantidad debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoCantidadEsNegativa()
        {
            var dto = new SaveCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = 1,
                Cantidad = -5
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La Cantidad debe ser mayor que cero.", result.Message);
        }
    }
}
