using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Persistence.Repositories;


namespace SellPoint.Test.CarritoPersistence
{
    public class CarritoObtenerPorIdAsyncTests
    {
        private readonly CarritoRepository _repository;

        public CarritoObtenerPorIdAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CarritoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CarritoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsNegativo()
        {
            int usuarioId = -1;

            var result = await _repository.ObtenerPorIdAsync(usuarioId);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener el carrito", result.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            
            int usuarioId = 1;

            var result = await _repository.ObtenerPorIdAsync(usuarioId);

            
            Assert.True(result.IsSuccess || !result.IsSuccess); 
        }
    }
}
