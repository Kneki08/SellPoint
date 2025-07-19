using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using ProductoServiceClass = SellPoint.Aplication.Services.ProductoService.ProductoService;

namespace SellPoint.Tests.Services
{
    public class ProductoObtenerTodosAsyncTests
    {
        private readonly Mock<IProductoRepository> _productoRepositoryMock;
        private readonly Mock<ILogger<ProductoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductoServiceClass _productoService;

        public ProductoObtenerTodosAsyncTests()
        {
            _productoRepositoryMock = new Mock<IProductoRepository>();
            _loggerMock = new Mock<ILogger<ProductoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _productoService = new ProductoServiceClass(
                _productoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarExito_CuandoRepositorioFunciona()
        {
            var expectedResult = OperationResult.Success("Productos obtenidos correctamente.");

            _productoRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ReturnsAsync(expectedResult);

            var result = await _productoService.ObtenerTodosAsync();

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            _productoRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ThrowsAsync(new Exception("Error inesperado"));

            var result = await _productoService.ObtenerTodosAsync();

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener todos los productos", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }
    }
}
