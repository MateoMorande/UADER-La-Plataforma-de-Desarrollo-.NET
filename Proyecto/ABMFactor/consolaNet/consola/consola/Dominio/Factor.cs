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
    public class Factor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FactorId { get; set; }
        public string Nombre { get; set; }
        public int estado { get; set; }
        public virtual ICollection<Valor> Valor { get; set; }
    }
}
