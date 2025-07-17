using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Persistence.Repositories;
using SellPoint.Aplication.Dtos.Cupon;
using System;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CuponRepositoryTests
{
    public class AgregarAsyncTests
    {
        private readonly CuponRepository _repository;

        public AgregarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CuponRepository>>();
            string fakeConnectionString = "Server=localhost;Database=fake_db;Uid=fake_user;Pwd=fake_pwd;";
            _repository = new CuponRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoCodigoEsVacio()
        {
            var dto = new SaveCuponDTO { Codigo = " ", ValorDescuento = 10, FechaVencimiento = DateTime.Now.AddDays(5) };
            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El código del cupón no puede estar vacío.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoValorDescuentoEsInvalido()
        {
            var dto = new SaveCuponDTO { Codigo = "DESCUENTO10", ValorDescuento = 0, FechaVencimiento = DateTime.Now.AddDays(5) };
            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El valor del descuento debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoFechaVencimientoEsPasada()
        {
            var dto = new SaveCuponDTO { Codigo = "DESCUENTO10", ValorDescuento = 15, FechaVencimiento = DateTime.Now.AddDays(-1) };
            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La fecha de vencimiento debe ser una fecha futura.", result.Message);
        }
    }
}