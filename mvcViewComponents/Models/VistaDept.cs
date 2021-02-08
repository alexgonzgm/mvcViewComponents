using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvcSession3.Models
{
    [Table("VISTADEPT")]
    public class VistaDept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("POSICION")]
        public int Posicion { get; set; }
        [Column("DEPT_NO")]
        public int Id { get; set; }
        [Column("DNOMBRE")]
        public string Nombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
       
    }
}
