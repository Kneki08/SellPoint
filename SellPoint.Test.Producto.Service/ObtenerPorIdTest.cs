using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Threading.Tasks;
using Xunit;
using ProductoServiceClass = SellPoint.Aplication.Services.ProductoService.ProductoService;

namespace SellPoint.Tests.Services
{
    public class ProductoObtenerPorIdAsyncTests
    {
        private readonly Mock<IProductoRepository> _productoRepositoryMock;
        private readonly Mock<ILogger<ProductoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductoServiceClass _productoService;

        public ProductoObtenerPorIdAsyncTests()
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
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            int productoId = 1;
            var expectedResult = OperationResult.Success("Producto obtenido correctamente.");

            _productoRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(productoId))
                .ReturnsAsync(expectedResult);

            var result = await _productoService.ObtenerPorIdAsync(productoId);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(productoId), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            int productoId = 0;

            var result = await _productoService.ObtenerPorIdAsync(productoId);

            Assert.False(result.IsSuccess);
            Assert.Equal("ID de producto inválido.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            int productoId = 2;

            _productoRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(productoId))
                .ThrowsAsync(new Exception("Error inesperado"));

            var result = await _productoService.ObtenerPorIdAsync(productoId);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener el producto", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(productoId), Times.Once);
        }
    }
}

