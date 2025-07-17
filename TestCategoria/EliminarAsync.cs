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
    public class EliminarAsyncTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CategoriaServiceClass _categoriaService;

        public EliminarAsyncTests()
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
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            // Arrange
            var dto = new RemoveCategoriaDTO
            {
                Id = 2
            };

            var expectedResult = OperationResult.Success("Categoría eliminada correctamente.");

            _categoriaRepositoryMock
                .Setup(repo => repo.EliminarAsync(dto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _categoriaService.EliminarAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            // Act
            var result = await _categoriaService.EliminarAsync(null!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveCategoriaDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            // Arrange
            var dto = new RemoveCategoriaDTO
            {
                Id = 0
            };

            // Act
            var result = await _categoriaService.EliminarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id de la categoría debe ser mayor que cero.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.EliminarAsync(It.IsAny<RemoveCategoriaDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoRepositorioFalla()
        {
            // Arrange
            var dto = new RemoveCategoriaDTO
            {
                Id = 5
            };

            _categoriaRepositoryMock
                .Setup(repo => repo.EliminarAsync(dto))
                .ThrowsAsync(new Exception("Error en la base de datos"));

            // Act
            var result = await _categoriaService.EliminarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error al eliminar la categoría", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.EliminarAsync(dto), Times.Once);
        }
    }
}

