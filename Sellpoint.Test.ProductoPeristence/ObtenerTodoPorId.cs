
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Persistence.Repositories;


namespace SellPoint.Tests.Persistence.ProductoRepositoryTests
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly ProductoRepository _repository;

        public ObtenerPorIdAsyncTests()
        {
            var loggerMock = new Mock<ILogger<ProductoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new ProductoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var result = await _repository.ObtenerPorIdAsync(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("Producto no encontrado.", result.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoProductoExiste()
        {


            int productoIdValido = 1; 

            var result = await _repository.ObtenerPorIdAsync(productoIdValido);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }
    }
}
