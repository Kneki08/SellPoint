using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCupon;


namespace SellPoint.View.Mappers.Cupon
{
    public interface ICuponMapper
    {
        CuponDTO Convert(CuponModel model);
        IEnumerable<CuponDTO> Convert(IEnumerable<CuponModel> models);
    }

    public class CuponMapper : ICuponMapper
    {
        public CuponDTO Convert(CuponModel model)
        {
            return new CuponDTO
            {
                Id = model.Id,
                Codigo = model.Codigo,
                Descripcion = model.Descripcion,
                TipoDescuento = model.TipoDescuento,
                ValorDescuento = model.ValorDescuento,
                MontoMinimo = model.MontoMinimo,
                FechaInicio = model.FechaInicio,
                FechaVencimiento = model.FechaVencimiento,
                UsosMaximos = model.UsosMaximos,
                UsosActuales = model.UsosActuales,
                Activo = model.Activo,
                FechaCreacion = model.FechaCreacion,
                FechaActualizacion = model.FechaActualizacion
            };
        }

        public IEnumerable<CuponDTO> Convert(IEnumerable<CuponModel> models)
        {
            return models.Select(Convert).ToList();
        }
    }
}

