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
    public class ObtenerTodosAsyncTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CategoriaServiceClass _categoriaService;

        public ObtenerTodosAsyncTests()
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
        public async Task ObtenerTodosAsync_DeberiaRetornarExito_CuandoHayCategorias()
        {
            // Arrange
            var expectedResult = OperationResult.Success("Categorías obtenidas correctamente.");

            _categoriaRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _categoriaService.ObtenerTodosAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            // Arrange
            _categoriaRepositoryMock
                .Setup(repo => repo.ObtenerTodosAsync())
                .ThrowsAsync(new Exception("Fallo al conectar a la base de datos"));

            // Act
            var result = await _categoriaService.ObtenerTodosAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al obtener las categorías", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }
    }
}

