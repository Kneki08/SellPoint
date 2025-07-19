using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using ProductoServiceClass = SellPoint.Aplication.Services.ProductoService.ProductoService;

namespace SellPoint.Tests.Services
{
    public class ProductoActualizarAsyncTests
    {
        private readonly Mock<IProductoRepository> _productoRepositoryMock;
        private readonly Mock<ILogger<ProductoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductoServiceClass _productoService;

        public ProductoActualizarAsyncTests()
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
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new UpdateProductoDTO
            {
                Id = 1,
                Nombre = "Mouse RGB",
                Precio = 45,
                CategoriaId = 1
            };

            var expectedResult = OperationResult.Success("Producto actualizado correctamente.");

            _productoRepositoryMock
                .Setup(repo => repo.ActualizarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _productoService.ActualizarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _productoService.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateProductoDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new UpdateProductoDTO
            {
                Id = 2,
                Nombre = "Teclado Mecánico",
                Precio = 70,
                CategoriaId = 2
            };

            _productoRepositoryMock
                .Setup(repo => repo.ActualizarAsync(dto))
                .ThrowsAsync(new Exception("Error en base de datos"));

            var result = await _productoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al actualizar el producto", result.Message);
            _productoRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }
    }
}
