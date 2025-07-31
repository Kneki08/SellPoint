using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Aplication.Dtos.Cupon;
using SellPoint.View.Models.ModelsCupon;

namespace SellPoint.View.Services.CuponApiClient
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
                ValorDescuento = model.ValorDescuento,
                FechaVencimiento = model.FechaVencimiento
            };
        }

        public IEnumerable<CuponDTO> Convert(IEnumerable<CuponModel> models)
        {
            return models.Select(Convert).ToList();
        }
    }
}
