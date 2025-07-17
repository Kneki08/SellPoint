using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Threading.Tasks;
using Xunit;
using CategoriaServiceClass = SellPoint.Application.Services.Categoria.CategoriaService;

namespace SellPoint.Tests.Services
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CategoriaServiceClass _categoriaService;

        public ObtenerPorIdAsyncTests()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _loggerMock = new Mock<ILogger<CategoriaServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _categoriaService = new CategoriaServiceClass(
                _categoriaRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoIdEsValido()
        {
            // Arrange
            int id = 1;
            var expectedResult = OperationResult.Success("Categoría obtenida correctamente.");

            _categoriaRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(id))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _categoriaService.ObtenerPorIdAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsMenorOIgualACero()
        {
            // Arrange
            int idInvalido = 0;

            // Act
            var result = await _categoriaService.ObtenerPorIdAsync(idInvalido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id de la categoría debe ser mayor que cero.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            int id = 10;

            _categoriaRepositoryMock
                .Setup(repo => repo.ObtenerPorIdAsync(id))
                .ThrowsAsync(new Exception("Fallo al conectar a la base de datos"));

            // Act
            var result = await _categoriaService.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener la categoría", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ObtenerPorIdAsync(id), Times.Once);
        }
    }
}

