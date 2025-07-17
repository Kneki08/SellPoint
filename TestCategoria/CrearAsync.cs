using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Threading.Tasks;
using Xunit;
// Alias para evitar conflicto de nombre entre namespace y clase
using CategoriaServiceClass = SellPoint.Application.Services.Categoria.CategoriaService;

namespace SellPoint.Tests.Services
{
    public class CategoriaServiceTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly CategoriaServiceClass _categoriaService;

        public CategoriaServiceTests()
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
        public async Task CrearAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new SaveCategoriaDTO
            {
                Nombre = "Tecnología",
                Descripcion = "Categoría de productos tecnológicos"
            };

            var expectedResult = OperationResult.Success("Categoría creada correctamente.");

            _categoriaRepositoryMock
                .Setup(repo => repo.AgregarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _categoriaService.CrearAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _categoriaService.CrearAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveCategoriaDTO>()), Times.Never);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoNombreEsVacio()
        {
            var dto = new SaveCategoriaDTO { Nombre = " " };

            var result = await _categoriaService.CrearAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El nombre de la categoría es obligatorio.", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SaveCategoriaDTO>()), Times.Never);
        }

        [Fact]
        public async Task CrearAsync_DeberiaRetornarError_CuandoRepositorioLanzaExcepcion()
        {
            var dto = new SaveCategoriaDTO { Nombre = "Deportes" };

            _categoriaRepositoryMock
                .Setup(repo => repo.AgregarAsync(dto))
                .ThrowsAsync(new Exception("Fallo en la base de datos"));

            var result = await _categoriaService.CrearAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("Error al crear la categoría", result.Message);
            _categoriaRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
        }
    }
}
