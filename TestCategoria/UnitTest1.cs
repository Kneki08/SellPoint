using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;

using SellPoint.Domain.Base;
using System.Threading.Tasks;
using Xunit;
using CategoriaServiceClass = SellPoint.Aplication.Services.CategoriaService.CategoriaService;

namespace SellPoint.Tests.CategoriaService
{
    public class ActualizarAsyncTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CategoriaServiceClass _categoriaService;

        public ActualizarAsyncTests()
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
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            // Arrange
            var dto = new UpdateCategoriaDTO
            {
                Id = 1,
                Nombre = "Electrónica",
                Descripcion = "Actualizada"
            };

            var expectedResult = OperationResult.Success("Categoría actualizada correctamente.");

            _categoriaRepositoryMock
                .Setup(repo => repo.ActualizarAsync(dto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _categoriaService.ActualizarAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Categoría actualizada correctamente.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            // Act
            var result = await _categoriaService.ActualizarAsync(null!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCategoriaDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsNegativo()
        {
            // Arrange
            var dto = new UpdateCategoriaDTO
            {
                Id = -3,
                Nombre = "Libros"
            };

            // Act
            var result = await _categoriaService.ActualizarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id de la categoría debe ser mayor que cero.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdateCategoriaDTO>()), Times.Never);
        }
    }
}
