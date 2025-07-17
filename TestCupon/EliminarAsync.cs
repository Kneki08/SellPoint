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
    public class EliminarAsyncTests
    {
        private readonly Mock<ICuponRepository> _cuponRepositoryMock;
        private readonly Mock<ILogger<CuponService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CuponService _cuponService;

        public EliminarAsyncTests()
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
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            // Arrange
            var cuponAEliminar = new RemoveCuponDTIO { Id = 5 };
            var expectedResult = OperationResult.Success("Cupón eliminado correctamente.");

            _cuponRepositoryMock
                .Setup(repo => repo.EliminarAsync(cuponAEliminar))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _cuponService.EliminarAsync(cuponAEliminar);

            // Assert
            Assert.True(result.IsSuccess);
            _cuponRepositoryMock.Verify(repo => repo.EliminarAsync(cuponAEliminar), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            // Act
            var result = await _cuponService.EliminarAsync(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveCuponDTIO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            // Arrange
            var cuponInvalido = new RemoveCuponDTIO { Id = 0 };

            // Act
            var result = await _cuponService.EliminarAsync(cuponInvalido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id del cupón debe ser mayor que cero.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveCuponDTIO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            var cuponAEliminar = new RemoveCuponDTIO { Id = 5 };

            _cuponRepositoryMock
                .Setup(repo => repo.EliminarAsync(cuponAEliminar))
                .ThrowsAsync(new Exception("Error en base de datos"));

            // Act
            var result = await _cuponService.EliminarAsync(cuponAEliminar);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al eliminar el cupón", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.EliminarAsync(cuponAEliminar), Times.Once);
        }
    }
}
