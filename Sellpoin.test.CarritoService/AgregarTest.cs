using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using Xunit;
using System.Threading.Tasks;
using CarritoServiceClass = SellPoint.Aplication.Services.CarritoService.CarritoService;
using SellPoint.Aplication.Dtos.Carrito;

namespace Sellpoin.test.CarritoService
{
    public class CarritoServiceTests
    {
        private readonly Mock<ICarritoRepository> _carritoRepositoryMock;
        private readonly Mock<ILogger<CarritoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CarritoServiceClass _carritoService;

        public CarritoServiceTests()
        {
            _carritoRepositoryMock = new Mock<ICarritoRepository>();
            _loggerMock = new Mock<ILogger<CarritoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _carritoService = new CarritoServiceClass(
                _carritoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new SaveCarritoDTO { UsuarioId = 1, ProductoId = 2, Cantidad = 3 };
            var expectedResult = OperationResult.Success("Carrito agregado correctamente");

            _carritoRepositoryMock.Setup(repo => repo.AgregarAsync(dto)).ReturnsAsync(expectedResult);

            var result = await _carritoService.AgregarAsync(dto);

            Assert.True(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _carritoService.AgregarAsync(null!);
            Assert.False(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveCarritoDTO>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new SaveCarritoDTO { UsuarioId = 1, ProductoId = 2, Cantidad = 3 };
            _carritoRepositoryMock.Setup(repo => repo.AgregarAsync(dto)).ThrowsAsync(new Exception("Error DB"));

            var result = await _carritoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al agregar el carrito", result.Message);
            _carritoRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }
    }

}