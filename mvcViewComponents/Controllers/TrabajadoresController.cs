using Microsoft.AspNetCore.Mvc;
using mvcSession3.Repositories;
using mvcViewComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcViewComponents.Controllers
{
    public class TrabajadoresController : Controller
    {
        private IRepositoryHospital repository;
        public TrabajadoresController(IRepositoryHospital repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PaginacionGrupoTrabajadores(int? posicion )
        {
            if (posicion == null)
            {
                posicion = 1;
            }
           
            int numeroregistros = 0;
            List<Trabajador> trabajadores =
                this.repository.GetGrupoTrabajadoresSql(posicion.Value ,ref numeroregistros);
            ViewData["REGISTROS"] = numeroregistros;
            return View(trabajadores);
        }
        [HttpPost]
        public IActionResult PaginacionGrupoTrabajadores(int? posicion, int? salario)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            if (salario == null)
            {
                salario = 0;
            }

            int numeroregistros = 0;
            List<Trabajador> trabajadores =
                this.repository.GetGrupoTrabajadoresSql(posicion.Value,salario.Value, ref numeroregistros);
            
            ViewData["REGISTROS"] = numeroregistros;
            return View(trabajadores);

           
        }
    }
}
