using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using CarritoServiceClass = SellPoint.Aplication.Services.CarritoService.CarritoService;

namespace Sellpoin.test.CarritoService
{
    public class CarritoServiceActualizarTests
    {
        private readonly Mock<ICarritoRepository> _carritoRepositoryMock;
        private readonly Mock<ILogger<CarritoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CarritoServiceClass _carritoService;

        public CarritoServiceActualizarTests()
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
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new UpdateCarritoDTO { UsuarioId = 1, ProductoId = 2, NuevaCantidad = 5 };
            var expectedResult = OperationResult.Success("Carrito actualizado correctamente");

            _carritoRepositoryMock.Setup(repo => repo.ActualizarAsync(dto)).ReturnsAsync(expectedResult);

            var result = await _carritoService.ActualizarAsync(dto);

            Assert.True(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _carritoService.ActualizarAsync(null!);
            Assert.False(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCarritoDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new UpdateCarritoDTO { UsuarioId = 1, ProductoId = 2, NuevaCantidad = 5 };
            _carritoRepositoryMock.Setup(repo => repo.ActualizarAsync(dto)).ThrowsAsync(new Exception("Error DB"));

            var result = await _carritoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al actualizar el carrito", result.Message);
            _carritoRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }
    }
}
