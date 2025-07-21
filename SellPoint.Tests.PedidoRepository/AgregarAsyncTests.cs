using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Domainn.Entities.Orders;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Tests.PedidoRepository
{
    public class AgregarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public AgregarAsyncTests()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SellPointTestDb;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString, loggerMock.Object);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var resultado = await _repository.AgregarAsync(null!);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.EntidadNula, resultado.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsInvalido()
        {
            var pedido = new Pedido
            {
                IdUsuario = 0,
                MetodoPago = MetodoPago.PayPal,
                Estado = EstadoPedido.EnPreparacion,
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 50,
                Total = 150,
                Fecha_creacion = DateTime.Now
            };

            var resultado = await _repository.AgregarAsync(pedido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.UsuarioIdInvalido, resultado.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsMuyLargo()
        {
            var pedido = new Pedido
            {
                IdUsuario = 1,
                MetodoPago = (MetodoPago)999, // Simula un valor fuera del enum definido
                Estado = EstadoPedido.EnPreparacion,
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 50,
                Total = 150,
                Fecha_creacion = DateTime.Now
            };

            var resultado = await _repository.AgregarAsync(pedido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoNoValido, resultado.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var pedido = new Pedido
            {
                IdUsuario = 1,
                MetodoPago = MetodoPago.PayPal,
                Estado = EstadoPedido.EnPreparacion,
                ReferenciaPago = new string('R', 101),
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 50,
                Total = 150,
                Fecha_creacion = DateTime.Now
            };

            var resultado = await _repository.AgregarAsync(pedido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, resultado.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoNotasSonMuyLargas()
        {
            var pedido = new Pedido
            {
                IdUsuario = 1,
                MetodoPago = MetodoPago.PayPal,
                Estado = EstadoPedido.EnPreparacion,
                Notas = new string('N', 501),
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 50,
                Total = 150,
                Fecha_creacion = DateTime.Now
            };

            var resultado = await _repository.AgregarAsync(pedido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, resultado.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoTotalEsInconsistente()
        {
            var pedido = new Pedido
            {
                IdUsuario = 1,
                MetodoPago = MetodoPago.PayPal,
                Estado = EstadoPedido.EnPreparacion,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 20,
                Total = 200, // incorrecto, debería ser 110
                Fecha_creacion = DateTime.Now
            };

            var resultado = await _repository.AgregarAsync(pedido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.TotalInconsistente, resultado.Message);
        }
    }
}