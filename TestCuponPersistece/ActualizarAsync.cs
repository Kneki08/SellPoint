using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.Domain.Base;
using SellPoint.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace SellPoint.Tests.Persistence.CuponRepositoryTests
{
    public class ActualizarAsyncTests
    {
        private readonly CuponRepository _repository;

        public ActualizarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<CuponRepository>>();
            string fakeConnectionString = "Server=localhost;Database=fake_db;Uid=fake_user;Pwd=fake_pwd;";
            _repository = new CuponRepository(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.ActualizarAsync(null!);
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsCero()
        {
            var dto = new UpdateCuponDTO { Id = 0 };
            var result = await _repository.ActualizarAsync(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoCodigoEsVacio()
        {
            var dto = new UpdateCuponDTO { Id = 1, Codigo = " " };
            var result = await _repository.ActualizarAsync(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("El código del cupón no puede estar vacío.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDescuentoEsInvalido()
        {
            var dto = new UpdateCuponDTO { Id = 1, Codigo = "ABC123", ValorDescuento = 0 };
            var result = await _repository.ActualizarAsync(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("El valor del descuento debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoFechaVencida()
        {
            var dto = new UpdateCuponDTO
            {
                Id = 1,
                Codigo = "ABC123",
                ValorDescuento = 10,
                FechaVencimiento = DateTime.Now.AddDays(-1)
            };
            var result = await _repository.ActualizarAsync(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("La fecha de vencimiento debe ser una fecha futura.", result.Message);
        }
    }
}