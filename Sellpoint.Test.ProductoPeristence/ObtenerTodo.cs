using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.ProductoRepositoryTests
{
    public class ObtenerTodosAsyncTests
    {
        private readonly ProductoRepository _repository;

        public ObtenerTodosAsyncTests()
        {
            var loggerMock = new Mock<ILogger<ProductoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new ProductoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarExito()
        {
            var result = await _repository.ObtenerTodosAsync();


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
        }
    }
}

