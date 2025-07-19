using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Persistence.Repositories;

namespace SellPoint.Test.CarritoPersistence
{
    public class CarritoActualizarAsyncTests
    {
        private readonly CarritoRepository _repository;

        public CarritoActualizarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CarritoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CarritoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoUsuarioIdEsNegativo()
        {
            var dto = new UpdateCarritoDTO
            {
                UsuarioId = -1,
                ProductoId = 1,
                NuevaCantidad = 2
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El UsuarioId no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoProductoIdEsNegativo()
        {
            var dto = new UpdateCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = -1,
                NuevaCantidad = 2
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ProductoId no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoCantidadEsCero()
        {
            var dto = new UpdateCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = 1,
                NuevaCantidad = 0
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La NuevaCantidad debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoCantidadEsNegativa()
        {
            var dto = new UpdateCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = 1,
                NuevaCantidad = -10
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La NuevaCantidad debe ser mayor que cero.", result.Message);
        }
    }
}
