using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using ProductoServiceClass = SellPoint.Aplication.Services.ProductoService.ProductoService;

namespace SellPoint.Tests.Services
{
    public class ProductoEliminarAsyncTests
    {
        private readonly Mock<IProductoRepository> _productoRepositoryMock;
        private readonly Mock<ILogger<ProductoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductoServiceClass _productoService;

        public ProductoEliminarAsyncTests()
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
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new RemoveProductoDTO { Id = 1 };

            var expectedResult = OperationResult.Success("Producto eliminado correctamente.");

            _productoRepositoryMock
                .Setup(repo => repo.EliminarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _productoService.EliminarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _productoService.EliminarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveProductoDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new RemoveProductoDTO { Id = 2 };

            _productoRepositoryMock
                .Setup(repo => repo.EliminarAsync(dto))
                .ThrowsAsync(new Exception("Error de base de datos"));

            var result = await _productoService.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al eliminar el producto", result.Message);
            _productoRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }
    }
}
