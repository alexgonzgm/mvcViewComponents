using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mvcSession3.Models;
using mvcSession3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcSession3.ViewComponents
{
    public class MenuDepartamentosViewComponent : ViewComponent
    {
        private IRepositoryHospital repository;
        public MenuDepartamentosViewComponent(IRepositoryHospital repository)
        {
            this.repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Departamento> departamentos = this.repository.GetDepartamentos();
            return View(departamentos);
        }
    }
}
