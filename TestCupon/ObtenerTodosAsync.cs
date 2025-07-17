using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Services.CuponService;
using SellPoint.Domain.Base;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SellPoint.Tests.Services
{
    public class ObtenerTodosAsyncTests
    {
        private readonly Mock<ICuponRepository> _cuponRepositoryMock;
        private readonly Mock<ILogger<CuponService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CuponService _cuponService;

        public ObtenerTodosAsyncTests()
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
        public async Task ObtenerTodosAsync_DeberiaRetornarExito_CuandoRepositorioRespondeCorrectamente()
        {
            // Arrange
            var expectedResult = OperationResult.Success("Operación exitosa.");

            _cuponRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _cuponService.ObtenerTodosAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            _cuponRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ThrowsAsync(new Exception("Error de base de datos"));

            // Act
            var result = await _cuponService.ObtenerTodosAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener los cupones", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }
    }
}
