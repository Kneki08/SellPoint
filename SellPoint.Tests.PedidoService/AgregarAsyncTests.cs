using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoService
{
    public class AgregarAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public AgregarAsyncTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _loggerMock = new Mock<ILogger<PedidoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _pedidoService = new PedidoServiceClass(
                _pedidoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
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
                Notas = "Pedido urgente"
            };

            var expectedResult = OperationResult.Success("Operación exitosa.");

            _pedidoRepositoryMock
                .Setup(repo => repo.AgregarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _pedidoService.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsMenorOIgualACero()
        {
            var dto = new SavePedidoDTO { UsuarioId = 0 };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del usuario debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoSubtotalEsNegativo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = -1,
                Descuento = 0,
                CostoEnvio = 0,
                Total = -1,
                MetodoPago = "Tarjeta"
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El subtotal no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoDescuentoEsNegativo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = -5,
                CostoEnvio = 0,
                Total = 95,
                MetodoPago = "Tarjeta"
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El descuento no puede ser negativo.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoCostoEnvioEsNegativo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = -5,
                Total = 95,
                MetodoPago = "Tarjeta"
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El costo de envío no puede ser negativo.", result.Message);
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
                Total = 80,
                MetodoPago = "Tarjeta"
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El total del pedido no coincide con la suma de subtotal, descuento y costo de envío.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsNulo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 0,
                Total = 100,
                MetodoPago = null
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El método de pago es obligatorio.", result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsMuyLargo()
        {
            var dto = new SavePedidoDTO
            {
                UsuarioId = 1,
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 0,
                Total = 100,
                MetodoPago = new string('A', 51)
            };

            var result = await _pedidoService.AgregarAsync(dto);

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
                Descuento = 0,
                CostoEnvio = 0,
                Total = 100,
                MetodoPago = "Tarjeta",
                ReferenciaPago = new string('B', 101)
            };

            var result = await _pedidoService.AgregarAsync(dto);

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
                Descuento = 0,
                CostoEnvio = 0,
                Total = 100,
                MetodoPago = "Tarjeta",
                Notas = new string('C', 501)
            };

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Las notas no deben superar los 500 caracteres.", result.Message);
        }
    }
}