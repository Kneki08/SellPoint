using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Domain.Base;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;

namespace SellPoint.Tests.PedidoRepository
{
    public class AgregarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public AgregarAsyncTests()
        {
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            string fakeConnectionString = "Server=(local);Database=FakeDB;Trusted_Connection=True;";
            _repository = new PedidoRepositoryClass(fakeConnectionString, loggerMock.Object);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsCero()
        {
            var dto = new SavePedidoDTO { UsuarioId = 0 };
            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El UsuarioId debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoTotalNoCoincide()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 5,
                Total = 80 // incorrecto
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El total no coincide con la suma de subtotal, descuento y costo de envío.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsMuyLargo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 5,
                Total = 95,
                MetodoPago = new string('X', 51)
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El método de pago no debe superar los 50 caracteres.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 5,
                Total = 95,
                MetodoPago = "Tarjeta",
                ReferenciaPago = new string('R', 101)
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La referencia de pago no debe superar los 100 caracteres.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoNotasSonMuyLargas()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 5,
                Total = 95,
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC123",
                Notas = new string('N', 501)
            };

            var result = await _repository.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Las notas no deben superar los 500 caracteres.", result.Message);
        }

        // Puedes agregar una prueba exitosa si decides mockear SqlConnection/SqlCommand.
    }
}