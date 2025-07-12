using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Persistence.Context
{
    public class sellpointContext : DbContext
    {
        public sellpointContext(DbContextOptions<sellpointContext> options) : base(options)
        {

        }

       
        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Cupon> Cupon { get; set; }
        

    }
}
