using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CategoriaRepositoryTests
{
    public class AgregarAsyncTests
    {
        private readonly CategoriaRepository _repository;

        public AgregarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CategoriaRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CategoriaRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoNombreEsVacio()
        {
            var dto = new SaveCategoriaDTO { Nombre = " " };
            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El nombre de la categoría no puede estar vacío.", result.Message);
        }

        
    }
}

