

namespace SellPoint.Aplication.Dtos.ProductoDTO
{
    public  record RemoveProductoDTO : DtoBase
    {
       public int Id { get; set; }
       public bool Remove { get; set; }
    }
}
