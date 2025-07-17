using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Persistence.Repositories;
using SellPoint.Aplication.Dtos.Cupon;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CuponRepositoryTests
{
    public class EliminarAsyncTests
    {
        private readonly CuponRepository _repository;

        public EliminarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CuponRepository>>();
            string fakeConnectionString = "Server=localhost;Database=fake_db;Uid=fake_user;Pwd=fake_pwd;";
            _repository = new CuponRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.EliminarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var dto = new RemoveCuponDTIO { Id = 0 };
            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id debe ser mayor que cero.", result.Message);
        }

    }
}

