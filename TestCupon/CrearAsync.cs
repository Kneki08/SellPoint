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
    public class CrearAsyncTests
    {
        private readonly Mock<ICuponRepository> _cuponRepositoryMock;
        private readonly Mock<ILogger<CuponService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CuponService _cuponService;

        public CrearAsyncTests()
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
        public async Task CrearAsync_DeberiaRetornarExito_CuandoDatosSonValidos()
        {
            // Arrange
            var nuevoCupon = new SaveCuponDTO
            {
                Codigo = "CUPON123"
                // Otros campos si los necesitas
            };

            var expectedResult = OperationResult.Success("Cupón creado correctamente.");

            _cuponRepositoryMock
                .Setup(repo => repo.AgregarAsync(nuevoCupon))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _cuponService.CrearAsync(nuevoCupon);

            // Assert
            Assert.True(result.IsSuccess);
            _cuponRepositoryMock.Verify(repo => repo.AgregarAsync(nuevoCupon), Times.Once);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoNuevoCuponEsNulo()
        {
            // Act
            var result = await _cuponService.CrearAsync(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveCuponDTO>()), Times.Never);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoCodigoEsNuloOVacio()
        {
            // Arrange
            var nuevoCupon = new SaveCuponDTO
            {
                Codigo = " " // Código inválido
            };

            // Act
            var result = await _cuponService.CrearAsync(nuevoCupon);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El código del cupón es obligatorio.", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveCuponDTO>()), Times.Never);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            var nuevoCupon = new SaveCuponDTO
            {
                Codigo = "CUPON123"
            };

            _cuponRepositoryMock
                .Setup(repo => repo.AgregarAsync(nuevoCupon))
                .ThrowsAsync(new Exception("Error en la base de datos"));

            // Act
            var result = await _cuponService.CrearAsync(nuevoCupon);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al crear el cupón", result.Message);
            _cuponRepositoryMock.Verify(repo => repo.AgregarAsync(nuevoCupon), Times.Once);
        }
    }
}