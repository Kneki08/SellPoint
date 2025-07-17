using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CategoriaRepositoryTests
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly CategoriaRepository _repository;

        public ObtenerPorIdAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CategoriaRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CategoriaRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsNegativo()
        {
            var result = await _repository.ObtenerPorIdAsync(-1);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id no puede ser negativo.", result.Message);
        }
    }
}

