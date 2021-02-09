using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mvcSession3.Data;
using mvcSession3.Models;
using mvcViewComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#region VISTA DEPARTAMENTOS PAGINACION

//CREATE VIEW VISTADEPT AS
//SELECT
//Cast(
// ISNULL(ROW_NUMBER () OVER(ORDER BY DEPT_NO),0)as int)
//AS POSICION
//, ISNULL(DEPT.DEPT_NO, 0) AS DEPT_NO
//, DEPT.DNOMBRE , DEPT.LOC FROM DEPT
// GO

//ALTER PROCEDURE PAGINARREGISTRODEPARTAMENTO
// (@POSICION INT , @REGISTROS INT OUT)
// AS
// SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
// SELECT DEPT_NO , DNOMBRE, LOC FROM VISTADEPT
// WHERE POSICION = @POSICION
// GO

//CREATE PROCEDURE PAGINARGRUPODEPARTAMENTOS 
// (@POSICION INT , @REGISTROS INT OUT)
// AS
// SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
// SELECT DEPT_NO , DNOMBRE, LOC FROM VISTADEPT
// WHERE POSICION >= @POSICION AND POSICION <(@POSICION +2)
// GO
/// vISTAS VARIAS
/// 
//CREATE VIEW VISTADEPT AS
//SELECT
//Cast(
// ISNULL(ROW_NUMBER () OVER(ORDER BY DEPT_NO),0)as int)
//AS POSICION
//, ISNULL(DEPT.DEPT_NO, 0) AS DEPT_NO
//, DEPT.DNOMBRE , DEPT.LOC FROM DEPT
// GO

// ALTER PROCEDURE PAGINARREGISTRODEPARTAMENTO
// (@POSICION INT , @REGISTROS INT OUT)
// AS
// SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
// SELECT DEPT_NO , DNOMBRE, LOC FROM VISTADEPT
// WHERE POSICION = @POSICION
// GO

// CREATE PROCEDURE PAGINARGRUPODEPARTAMENTOS 
// (@POSICION INT , @REGISTROS INT OUT)
// AS
// SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
// SELECT DEPT_NO , DNOMBRE, LOC FROM VISTADEPT
// WHERE POSICION >= @POSICION AND POSICION <(@POSICION +2)
// GO

// CREATE VIEW TRABAJADORES  AS
//SELECT ISNULL(EMP_NO , 0) AS IDTRABAJADOR, APELLIDO, OFICIO, SALARIO FROM EMP
//INNER JOIN DEPT ON EMP.DEPT_NO = DEPT.DEPT_NO
//UNION 
//SELECT DOCTOR_NO AS IDTRABAJADOR , APELLIDO, ESPECIALIDAD, SALARIO FROM DOCTOR
//INNER JOIN HOSPITAL ON DOCTOR.HOSPITAL_COD = HOSPITAL.HOSPITAL_COD
//UNION
//SELECT EMPLEADO_NO AS IDTRABAJADOR , APELLIDO, FUNCION, SALARIO FROM PLANTILLA
//INNER JOIN HOSPITAL ON PLANTILLA.HOSPITAL_COD = HOSPITAL.HOSPITAL_COD
//GO

//CREATE VIEW POSICIONTRABAJADORES
//AS 
// SELECT CAST(ISNULL(ROW_NUMBER() OVER(ORDER BY IDTRABAJADOR),0)AS INT) AS POSICION
// , IDTRABAJADOR, APELLIDO, OFICIO, SALARIO FROM TRABAJADORES
//GO

//alter procedure PAGINACIONGRUPOSTRABAJADORES 
//(@POSICION INT , @REGISTROS INT OUT)
//AS
//SELECT @REGISTROS = COUNT(IDTRABAJADOR) FROM POSICIONTRABAJADORES
//SELECT IDTRABAJADOR , APELLIDO, OFICIO, SALARIO FROM POSICIONTRABAJADORES
//WHERE POSICION >= @POSICION AND POSICION <(@POSICION + 4) 
//GO

//alter procedure PAGINACIONGRUPOSTRABAJADORESFILTROSALARIO 
//(@POSICION INT, @SALARIO INT , @REGISTROS INT OUT)
//AS
//SELECT @REGISTROS = COUNT(IDTRABAJADOR) FROM POSICIONTRABAJADORES where SALARIO >=@SALARIO
//SELECT IDTRABAJADOR , APELLIDO, OFICIO, SALARIO FROM POSICIONTRABAJADORES
//WHERE POSICION >= @POSICION AND POSICION <(@POSICION + 4) AND SALARIO >= @SALARIO
//GO


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

        public Departamento GetDepartamentoPosicion(int posicion ,ref int salida)
        {
            var sql = "PAGINARREGISTRODEPARTAMENTO  @POSICION , @REGISTROS OUT";
            SqlParameter pPosicion = new SqlParameter("@POSICION", posicion);
            SqlParameter pRegistros = new SqlParameter("@REGISTROS",-1);
            pRegistros.Direction = System.Data.ParameterDirection.Output;
            Departamento departamento =
                this.context.Departamentos.FromSqlRaw(sql, pPosicion, pRegistros).ToList().FirstOrDefault();
            int numeroregistros = Convert.ToInt32(pRegistros.Value);
            salida = numeroregistros;
            return departamento;
        }

        public List<Departamento> GetGrupoDepartamentosSql(int posicion, ref int numeroregistros)
        {
            var sql = "PAGINARGRUPODEPARTAMENTOS @POSICION , @REGISTROS OUT";
            SqlParameter pPosicion = new SqlParameter("@POSICION",posicion);
            SqlParameter pRegistros = new SqlParameter("@REGISTROS", -1);
            pRegistros.Direction = System.Data.ParameterDirection.Output;
            List<Departamento> departamentos =
                this.context.Departamentos.FromSqlRaw(sql, pPosicion, pRegistros).ToList();
            //int numeroRegistros = Convert.ToInt32(pRegistros.Value);
            //numeroregistros = numeroRegistros;
            numeroregistros = Convert.ToInt32(pRegistros.Value);
            return departamentos;
        }

        public List<Trabajador> GetGrupoTrabajadoresSql(int posicion,ref int registros)
        {
            var sql = "PAGINACIONGRUPOSTRABAJADORES @POSICION , @REGISTROS OUT";
            SqlParameter pPosicion = new SqlParameter("@POSICION", posicion);
            SqlParameter pRegistros = new SqlParameter("@REGISTROS", -1);
            pRegistros.Direction = System.Data.ParameterDirection.Output;
            List<Trabajador> trabajadores =
                this.context.Trabajadores.FromSqlRaw(sql, pPosicion,pRegistros).ToList();
            registros = Convert.ToInt32(pRegistros.Value);
            return trabajadores;
        }

        public List<Trabajador> GetGrupoTrabajadoresSql(int posicion,int salario, ref int registros)
        {
             var sql = "PAGINACIONGRUPOSTRABAJADORESFILTROSALARIO @POSICION , @SALARIO , @REGISTROS OUT";
            SqlParameter pPosicion = new SqlParameter("@POSICION", posicion);
            SqlParameter pSalario = new SqlParameter("@SALARIO", salario);
            SqlParameter pRegistros = new SqlParameter("@REGISTROS", -1);
            pRegistros.Direction = System.Data.ParameterDirection.Output;
            List<Trabajador> trabajadores =
                this.context.Trabajadores.FromSqlRaw(sql, pPosicion, pSalario,pRegistros).ToList();
            registros = Convert.ToInt32(pRegistros.Value);
            return trabajadores;
        }
    }
}
