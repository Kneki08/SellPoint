using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.ProductoDTO;
using SellPoint.Persistence.Repositories;


namespace SellPoint.Tests.Persistence.ProductoRepositoryTests
{
    public class EliminarAsyncTests
    {
        private readonly ProductoRepository _repository;

        public EliminarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<ProductoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new ProductoRepository(fakeConnectionString, loggerMock.Object);
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
            var dto = new RemoveProductoDTO
            {
                Id = -1,
                EsEliminado = true
            };

            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("El Id no puede ser negativo", result.Message);
        }
    }
}

