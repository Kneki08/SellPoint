using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using ProductoServiceClass = SellPoint.Aplication.Services.ProductoService.ProductoService;

namespace SellPoint.Tests.Services
{
    public class ProductoServiceTests
    {
        private readonly Mock<IProductoRepository> _productoRepositoryMock;
        private readonly Mock<ILogger<ProductoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductoServiceClass _productoService;

        public ProductoServiceTests()
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
        public async Task AgregarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new SaveProductoDTO
            {
                Nombre = "Mouse Gamer",
                Precio = 50,
                CategoriaId = 1
            };

            var expectedResult = OperationResult.Success("Producto agregado correctamente.");

            _productoRepositoryMock
                .Setup(repo => repo.AgregarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _productoService.AgregarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _productoService.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _productoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveProductoDTO>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new SaveProductoDTO
            {
                Nombre = "Laptop Gamer",
                Precio = 1000,
                CategoriaId = 2
            };

            _productoRepositoryMock
                .Setup(repo => repo.AgregarAsync(dto))
                .ThrowsAsync(new Exception("Fallo en base de datos"));

            var result = await _productoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al agregar el producto", result.Message);
            _productoRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }
    }
}
