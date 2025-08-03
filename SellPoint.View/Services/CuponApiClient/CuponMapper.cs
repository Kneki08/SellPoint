using SellPoint.View.Models.ModelsCupon;

namespace SellPoint.View.Mappers.Cupon
{
    public interface ICuponMapper
    {
        CuponModel Convert(CuponModel model); 
        IEnumerable<CuponModel> Convert(IEnumerable<CuponModel> models);
    }

    public class CuponMapper : ICuponMapper
    {
        public CuponModel Convert(CuponModel model)
        {
            return model; 
        }

        public IEnumerable<CuponModel> Convert(IEnumerable<CuponModel> models)
        {
            return models;
        }
    }
}

