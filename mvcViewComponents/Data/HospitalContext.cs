using Microsoft.EntityFrameworkCore;
using mvcSession3.Models;
using mvcViewComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcSession3.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<VistaDept> VistaDepartamentos { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
    }
}
