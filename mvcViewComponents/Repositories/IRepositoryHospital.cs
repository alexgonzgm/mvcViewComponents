using mvcSession3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcSession3.Repositories
{  
     public interface IRepositoryHospital
     {
        List<Departamento> GetDepartamentos();
        Departamento FindDepartamento(int id);
        VistaDept GetRegistroDepartamento(int posicion);
        int GetNumeroRegistrosVistaDepartamentos();
        List<VistaDept> GetGrupoDepartamentos(int posicion);
     }
}
