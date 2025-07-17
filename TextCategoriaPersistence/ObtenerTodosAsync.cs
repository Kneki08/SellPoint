using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CategoriaRepositoryTests
{
    public class ObtenerTodosAsyncTests
    {
        private readonly CategoriaRepository _repository;

        public ObtenerTodosAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CategoriaRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CategoriaRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarExito()
        {
            var result = await _repository.ObtenerTodosAsync();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }
    }
}

