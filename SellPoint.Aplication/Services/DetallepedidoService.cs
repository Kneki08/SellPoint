using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Base;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Aplication.Interfaces.Servicios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;

namespace SellPoint.Aplication.Services.DetallepedidoService
{
    public class DetallepedidoService : BaseService<DetallePedido, SaveDetallePedidoDTO, UpdateDetallePedidoDTO, RemoveDetallePedidoDTO>, IDetallepedidoService
    {
        public DetallepedidoService( DbContext context, ILogger<DetallepedidoService> logger)
            : base(context, logger)
        {
        }

        protected override DetallePedido MapSaveDtoToEntity(SaveDetallePedidoDTO dto) =>
            new DetallePedido
            {
                Pedidoid = dto.PedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
              
            };

        protected override DetallePedido MapUpdateDtoToEntity(UpdateDetallePedidoDTO dto) =>
            new DetallePedido
            {
                
                Pedidoid = dto.PedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                
            };

        protected override DetallePedido MapRemoveDtoToEntity(RemoveDetallePedidoDTO dto) =>
            new DetallePedido
            {
                Pedidoid = dto.Id
            };
    }
}
