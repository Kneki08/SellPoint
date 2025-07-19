using Microsoft.EntityFrameworkCore;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Domainn.Entities.Orders;
using SellPoint.Domainn.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Persistence.Context
{
    internal class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Cupon> Cupon { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Pedido> Pedido { get; set; }


    }
}
