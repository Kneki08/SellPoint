using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.ProductoRepositoryTests
{
    public class ActualizarAsyncTests
    {
        private readonly ProductoRepository _repository;

        public ActualizarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<ProductoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new ProductoRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNombreEsVacio()
        {
            var dto = new UpdateProductoDTO
            {
                Id = 1,
                Nombre = " ",
                Descripcion = "Producto sin nombre",
                Precio = 100,
                Stock = 10,
                Activo = true
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Contains("El nombre del producto no puede estar vacío.", result.Message);
        }
    }
}

