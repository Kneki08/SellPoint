using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CategoriaRepositoryTests
{
    public class ActualizarAsyncTests
    {
        private readonly CategoriaRepository _repository;

        public ActualizarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CategoriaRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CategoriaRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsNegativo()
        {
            var dto = new UpdateCategoriaDTO { Id = -1, Nombre = "Categoria Test" };
            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id de la categoría no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNombreEsVacio()
        {
            var dto = new UpdateCategoriaDTO { Id = 1, Nombre = " " };
            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El nombre de la categoría no puede estar vacío.", result.Message);
        }
    }
}

