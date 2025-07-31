using System;
using SellPoint.View.Models.Pedido;
using SellPoint.View.Models.ViewModels;

namespace SellPoint.View.Mappers
{
    public static class PedidoViewModelMapper
    {
        // De DTO a ViewModel
        public static PedidoViewModel ToViewModel(PedidoDTO dto)
        {
            return new PedidoViewModel
            {
                Id = dto.Id,
                IdUsuario = dto.IdUsuario,
                IdDireccionEnvio = dto.IdDireccionEnvio,
                MetodoPago = dto.MetodoPago,
                ReferenciaPago = dto.ReferenciaPago,
                Subtotal = dto.Subtotal,
                Descuento = dto.Descuento,
                CostoEnvio = dto.CostoEnvio,
                Total = dto.Total,
                Estado = dto.Estado,
                FechaPedido = dto.FechaPedido,
                CuponId = dto.CuponId,
                Notas = dto.Notas
            };
        }

        // De ViewModel a SavePedidoDTO (para agregar)
        public static SavePedidoDTO ToSaveDTO(PedidoViewModel vm)
        {
            return new SavePedidoDTO
            {
                IdUsuario = vm.IdUsuario,
                IdDireccionEnvio = vm.IdDireccionEnvio,
                MetodoPago = vm.MetodoPago,
                ReferenciaPago = vm.ReferenciaPago,
                Subtotal = vm.Subtotal,
                Descuento = vm.Descuento,
                CostoEnvio = vm.CostoEnvio,
                Total = vm.Total,
                Estado = vm.Estado,
                FechaPedido = vm.FechaPedido,
                CuponId = vm.CuponId,
                Notas = vm.Notas
            };
        }

        // De ViewModel a UpdatePedidoDTO (para actualizar)
        public static UpdatePedidoDTO ToUpdateDTO(PedidoViewModel vm)
        {
            return new UpdatePedidoDTO
            {
                Id = vm.Id,
                IdUsuario = vm.IdUsuario,
                IdDireccionEnvio = vm.IdDireccionEnvio,
                MetodoPago = vm.MetodoPago,
                ReferenciaPago = vm.ReferenciaPago,
                Subtotal = vm.Subtotal,
                Descuento = vm.Descuento,
                CostoEnvio = vm.CostoEnvio,
                Total = vm.Total,
                Estado = vm.Estado,
                FechaPedido = vm.FechaPedido,
                FechaActualizacion = DateTime.Now,
                CuponId = vm.CuponId,
                Notas = vm.Notas
            };
        }
    }
}