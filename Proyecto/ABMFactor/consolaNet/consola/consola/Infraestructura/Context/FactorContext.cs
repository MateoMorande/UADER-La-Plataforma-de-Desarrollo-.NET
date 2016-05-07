using consola.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consola.Context
{
    public class FactorContext : DbContext
    {
        public DbSet<Factor> Factores { get; set; }
        public DbSet<Valor> Valores { get; set; }
    }
}
