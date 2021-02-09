using mvcSession3.Models;
using mvcViewComponents.Models;
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
        Departamento GetDepartamentoPosicion(int posicion , ref int salida);
        List<Departamento> GetGrupoDepartamentosSql(int posicion, ref int numeroregistros);
        List<Trabajador> GetGrupoTrabajadoresSql(int posicion, ref int registros);
        List<Trabajador> GetGrupoTrabajadoresSql(int posicion, int salario, ref int registros);

    }
}
