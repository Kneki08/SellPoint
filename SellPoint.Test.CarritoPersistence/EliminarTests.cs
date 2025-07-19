using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Persistence.Repositories;


namespace SellPoint.Test.CarritoPersistence
{
    public class CarritoEliminarAsyncTests
    {
        private readonly CarritoRepository _repository;

        public CarritoEliminarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CarritoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CarritoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.EliminarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoUsuarioIdEsNegativo()
        {
            var dto = new RemoveCarritoDTO
            {
                UsuarioId = -1,
                ProductoId = 1
            };

            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El UsuarioId no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoProductoIdEsNegativo()
        {
            var dto = new RemoveCarritoDTO
            {
                UsuarioId = 1,
                ProductoId = -2
            };

            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ProductoId no puede ser negativo.", result.Message);
        }
    }
}
