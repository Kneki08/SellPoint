using Microsoft.EntityFrameworkCore;
using SellPoint.Domain.Entities;
using SellPoint.Domainn.Entities.Orders;

namespace SellPoint.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; } = null!;
    }
}