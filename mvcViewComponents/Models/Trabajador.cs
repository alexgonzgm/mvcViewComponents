using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvcViewComponents.Models
{
    [Table("TRABAJADORES")]
    public class Trabajador
    {  [Key]
        public int IdTrabajador { get; set; }
        public string Apellido { get; set; }
        public string Oficio { get; set; }
        public int Salario { get; set; }
    }
}
