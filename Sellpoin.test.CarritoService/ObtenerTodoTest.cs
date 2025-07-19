using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using CarritoServiceClass = SellPoint.Aplication.Services.CarritoService.CarritoService;

namespace Sellpoin.test.CarritoService
{
    public class CarritoServiceObtenerTodoTests
    {
        private readonly Mock<ICarritoRepository> _carritoRepositoryMock;
        private readonly Mock<ILogger<CarritoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CarritoServiceClass _carritoService;

        public CarritoServiceObtenerTodoTests()
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
        public async Task ObtenerTodosAsync_DeberiaRetornarExito_CuandoRepositorioFunciona()
        {
            var expectedResult = OperationResult.Success("Carritos obtenidos correctamente");
            _carritoRepositoryMock.Setup(repo => repo.ObtenerTodosAsync()).ReturnsAsync(expectedResult);

            var result = await _carritoService.ObtenerTodosAsync();
            Assert.True(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            _carritoRepositoryMock.Setup(repo => repo.ObtenerTodosAsync()).ThrowsAsync(new Exception("Error DB"));

            var result = await _carritoService.ObtenerTodosAsync();

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener todos los carritos", result.Message);
            _carritoRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }
    }
}
