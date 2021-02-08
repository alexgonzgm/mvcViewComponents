using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcSession3.Models;
using mvcSession3.Repositories;

namespace mvcSession3.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repository;
        public DepartamentosController(IRepositoryHospital repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Departamento departamento = this.repository.FindDepartamento(id);
            return View(departamento);
        }

        public IActionResult PaginarVistaDeptRegistro(int? posicion)
        {
            //si no existe posicion mostramos el primer registro 
            if (posicion == null )
            {
                posicion = 1; 
            }
            int ultimaposicion = this.repository.GetNumeroRegistrosVistaDepartamentos();
            int siguiente = posicion.Value + 1;
            //DEBEMOS COMPROBAR QUE NO NOS PASAMOS 
            if (siguiente > ultimaposicion)
            {
                siguiente = ultimaposicion;
            }
            int anterior = posicion.Value - 1;
            if (anterior <1 )
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = ultimaposicion;
            ViewData["SIQUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            VistaDept dept = this.repository.GetRegistroDepartamento(posicion.Value);
           return View(dept);
            
        }
        public  IActionResult PaginarVistaDeptGrupo(int? posicion)
        {
            //COMPROBAMOS IS HEMOS RECIBIDO POSICION , SI NO ES LA PRIMERA 
            if (posicion == null)
            {
                posicion = 1;

            }
            int numeropagina = 1;
            int numeroregistros = this.repository.GetNumeroRegistrosVistaDepartamentos();
            //BUCLE QUE IRA DESDE LA POSICION UNO HASTA NUMERO DE REGISTROS 
            // MOVIENDOSE ENTRE LOS ELEMENTOS PAGINADOS(2)
            string html = "<div>";
            for (int i = 1; i < numeroregistros; i+=2)
            {
                html += "<a href='PaginarVistaDeptGrupo?posicion=" + i + "'>" + numeropagina + "</a>| ";
                numeropagina += 1;
            }
            html += "</div>";
            ViewData["PAGINAS"] = html;
            List<VistaDept> departamentos = this.repository.GetGrupoDepartamentos(posicion.Value);
            return View(departamentos);
        }
    }
}