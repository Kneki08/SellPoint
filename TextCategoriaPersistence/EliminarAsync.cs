using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CategoriaRepositoryTests
{
    public class EliminarAsyncTests
    {
        private readonly CategoriaRepository _repository;

        public EliminarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CategoriaRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CategoriaRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.EliminarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsNegativo()
        {
            var dto = new RemoveCategoriaDTO { Id = -1 };
            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id de la categoría no puede ser negativo.", result.Message);
        }
    }
}

