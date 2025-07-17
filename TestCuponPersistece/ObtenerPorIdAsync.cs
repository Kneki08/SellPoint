using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CuponRepositoryTests
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly CuponRepository _repository;

        public ObtenerPorIdAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CuponRepository>>();
            string fakeConnectionString = "Server=localhost;Database=fake_db;Uid=fake_user;Pwd=fake_pwd;";
            _repository = new CuponRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var result = await _repository.ObtenerPorIdAsync(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id debe ser mayor que cero.", result.Message);
        }

      
    }
}
