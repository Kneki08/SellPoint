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
    public class ActualizarAsyncTests
    {
        private readonly Mock<ICuponRepository> _cuponRepositoryMock;
        private readonly Mock<ILogger<CuponService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CuponService _cuponService;

        public ActualizarAsyncTests()
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
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDatosSonValidos()
        {
            // Arrange
            var cuponActualizado = new UpdateCuponDTO
            {
                Id = 10,
                Codigo = "ACTUALIZADO123"
                // Otros campos si aplica
            };

            var expectedResult = OperationResult.Success("Cupón actualizado correctamente.");

            _cuponRepositoryMock
                .Setup(repo => repo.ActualizarAsync(cuponActualizado))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _cuponService.ActualizarAsync(cuponActualizado);

            // Assert
            Assert.True(result.IsSuccess);
            _cuponRepositoryMock.Verify(repo => repo.ActualizarAsync(cuponActualizado), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            // Act
            var result = await _cuponService.ActualizarAsync(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCuponDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            // Arrange
            var cuponInvalido = new UpdateCuponDTO
            {
                Id = 0,
                Codigo = "VALIDO123"
            };

            // Act
            var result = await _cuponService.ActualizarAsync(cuponInvalido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id del cupón debe ser mayor que cero.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCuponDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoCodigoEsNuloOVacio()
        {
            // Arrange
            var cuponInvalido = new UpdateCuponDTO
            {
                Id = 1,
                Codigo = " " // inválido
            };

            // Act
            var result = await _cuponService.ActualizarAsync(cuponInvalido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El código del cupón es obligatorio.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCuponDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            var cuponActualizado = new UpdateCuponDTO
            {
                Id = 5,
                Codigo = "EXCEPCION"
            };

            _cuponRepositoryMock
                .Setup(repo => repo.ActualizarAsync(cuponActualizado))
                .ThrowsAsync(new Exception("Base de datos caída"));

            // Act
            var result = await _cuponService.ActualizarAsync(cuponActualizado);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al actualizar el cupón", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.ActualizarAsync(cuponActualizado), Times.Once);
        }
    }
}

