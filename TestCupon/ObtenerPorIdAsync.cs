using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Services.CuponService;
using SellPoint.Domain.Base;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SellPoint.Tests.Services
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly Mock<ICuponRepository> _cuponRepositoryMock;
        private readonly Mock<ILogger<CuponService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CuponService _cuponService;

        public ObtenerPorIdAsyncTests()
        {
            _cuponRepositoryMock = new Mock<ICuponRepository>();
            _loggerMock = new Mock<ILogger<CuponService>>();
            _configurationMock = new Mock<IConfiguration>();

            _cuponService = new CuponService(
                _cuponRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            // Arrange
            int cuponId = 1;
            var expectedResult = OperationResult.Success("Cupón obtenido correctamente.");

            _cuponRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(cuponId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _cuponService.ObtenerPorIdAsync(cuponId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(cuponId), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            // Arrange
            int cuponId = 0;

            // Act
            var result = await _cuponService.ObtenerPorIdAsync(cuponId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id del cupón debe ser mayor que cero.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            int cuponId = 999;

            _cuponRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(cuponId))
                .ThrowsAsync(new Exception("Error de conexión"));

            // Act
            var result = await _cuponService.ObtenerPorIdAsync(cuponId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener el cupón", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(cuponId), Times.Once);
        }
    }
}
