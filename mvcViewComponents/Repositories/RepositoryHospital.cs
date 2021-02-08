using mvcSession3.Data;
using mvcSession3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#region VISTA DEPARTAMENTOS PAGINACION

//CREATE VIEW VISTADEPT AS
//SELECT
// ISNULL(ROW_NUMBER () OVER (ORDER BY DEPT_NO),0)
//AS POSICION
//, ISNULL(DEPT.DEPT_NO, 0) AS DEPT_NO
//, DEPT.DNOMBRE , DEPT.LOC FROM DEPT
// GO
#endregion
namespace mvcSession3.Repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public Departamento FindDepartamento(int id)
        {
            return this.context.Departamentos.Where(c => c.Id == id).SingleOrDefault();
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public List<VistaDept> GetGrupoDepartamentos(int posicion)
        {
            var consulta = from datos in this.context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;
            return consulta.ToList();


        }

        public int GetNumeroRegistrosVistaDepartamentos()
        {
            return this.context.VistaDepartamentos.Count();
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            VistaDept vista = 
                this.context.VistaDepartamentos
                .Where(x => x.Posicion == posicion)
                .FirstOrDefault();
            return vista;
        }
    }
}
