
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using Xunit;
using CarritoServiceClass = SellPoint.Aplication.Services.CarritoService.CarritoService;    

namespace Sellpoin.test.CarritoService
{

    public class CarritoServiceEliminarTests
    {
        private readonly Mock<ICarritoRepository> _carritoRepositoryMock;
        private readonly Mock<ILogger<CarritoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CarritoServiceClass _carritoService;

        public CarritoServiceEliminarTests()
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
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new RemoveCarritoDTO { UsuarioId = 1, ProductoId = 2 };
            var expectedResult = OperationResult.Success("Carrito eliminado correctamente");

            _carritoRepositoryMock.Setup(repo => repo.EliminarAsync(dto)).ReturnsAsync(expectedResult);

            var result = await _carritoService.EliminarAsync(dto);

            Assert.True(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _carritoService.EliminarAsync(null!);
            Assert.False(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveCarritoDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new RemoveCarritoDTO { UsuarioId = 1, ProductoId = 2 };
            _carritoRepositoryMock.Setup(repo => repo.EliminarAsync(dto)).ThrowsAsync(new Exception("Error DB"));

            var result = await _carritoService.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al eliminar el carrito", result.Message);
            _carritoRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }
    }
}
