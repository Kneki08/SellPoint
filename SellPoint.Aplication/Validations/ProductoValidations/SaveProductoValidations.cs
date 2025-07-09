using SellPoint.Aplication.Dtos.Carrito;
using SellPoint.Aplication.Dtos.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Validations.ProductoValidations
{
   public class SaveProductoValidations : ValidatorBase<SaveProductoDTO>
    {
        public override bool Validate(SaveProductoDTO dto, out List<string> errors)
        {
            errors = new List<string>();
            if (dto is null)
            {
                errors.Add("La entidad no puede ser nula.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre del producto no puede estar vacío.");
            if (dto.Precio <= 0)
                errors.Add("El precio del producto debe ser mayor que cero.");
            if (dto.Stock < 0)
                errors.Add("El stock del producto no puede ser negativo.");
            if (dto.CategoriaId <= 0)
                errors.Add("El Id de la categoría debe ser mayor que cero.");
            return errors.Count == 0;
        }

    }
}
