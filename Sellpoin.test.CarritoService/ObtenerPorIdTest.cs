using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoServiceClass = SellPoint.Aplication.Services.CarritoService.CarritoService;

namespace Sellpoin.test.CarritoService
{
    public class CarritoServiceObtenerIDTests
    {
        private readonly Mock<ICarritoRepository> _carritoRepositoryMock;
        private readonly Mock<ILogger<CarritoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CarritoServiceClass _carritoService;

        public CarritoServiceObtenerIDTests()
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
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            int id = 1;
            var expectedResult = OperationResult.Success("Carrito encontrado correctamente");

            _carritoRepositoryMock.Setup(repo => repo.ObtenerPorIdAsync(id)).ReturnsAsync(expectedResult);

            var result = await _carritoService.ObtenerPorIdAsync(id);

            Assert.True(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            int id = 0;
            var result = await _carritoService.ObtenerPorIdAsync(id);
            Assert.False(result.IsSuccess);
            _carritoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            int id = 1;
            _carritoRepositoryMock.Setup(repo => repo.ObtenerPorIdAsync(id)).ThrowsAsync(new Exception("Error DB"));

            var result = await _carritoService.ObtenerPorIdAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener el carrito por ID", result.Message);
            _carritoRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(id), Times.Once);
        }
    }
}
