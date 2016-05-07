using consola.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consola.Model
{
    public class Valor
    {
        [Key,Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ValorId { get; set; }
        [Key, Column(Order = 1)]
        public int FactorId { get; set; }
        public string Descripcion { get; set; }
        public int valor { get; set; }
        public virtual Factor Factor { get; set; }
    }
}
