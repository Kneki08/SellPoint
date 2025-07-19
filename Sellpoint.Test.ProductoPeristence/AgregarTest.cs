
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Producto;
using SellPoint.Persistence.Repositories;


namespace SellPoint.Tests.Persistence.ProductoRepositoryTests
{
    public class AgregarAsyncTests
    {
        private readonly ProductoRepository _repository;

        public AgregarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<ProductoRepository>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new ProductoRepository(fakeConnectionString, loggerMock.Object);
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
            var dto = new SaveProductoDTO
            {
                Nombre = " ",
                Descripcion = "Producto sin nombre",
                Precio = 100,
                Stock = 5,
                Activo = true
            };

            var result = await _repository.AgregarAsync(dto);


            Assert.False(result.IsSuccess);
            Assert.Contains("La entidad no puede ser nula.", result.Message);
        }
    }
}

