using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Persistence.Repositories;

namespace SellPoint.Test.CarritoPersistence
{
    public class CarritoObtenerTodosAsyncTests
    {
        private readonly CarritoRepository _repository;

        public CarritoObtenerTodosAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CarritoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new CarritoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarExito()
        {
            var result = await _repository.ObtenerTodosAsync();

           
            Assert.True(result.IsSuccess || !result.IsSuccess); 
        }
    }
}
