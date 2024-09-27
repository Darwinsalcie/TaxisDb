using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Role;
using TaxisDb.Web.Models.Role;

namespace TaxisDb.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository roleRepository;


        // GET: RoleController

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }


        public async Task<IActionResult> Index()
        {
            var result = await this.roleRepository.GetRoles();

            return View(result);
        }



        // GET: RoleController/GetByRolename/5
        //public async Task<IActionResult> GetByRoleName(string name)
        //{
        //    var result = await this.roleRepository.GetRoles(name);


        //    return View("Index", new DataResults<List<RoleModel>>
        //    {
        //        Result = new List<RoleModel> { result.Result }
        //    });
        //}


        // GET: RoleController/Details/5
        public async Task<IActionResult> Details(int Id)
        {

            var result = await this.roleRepository.GetRolesById(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el rol
            }
            return View(result.Result);

        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleSaveDTO saveDTO)
        {
            try
            {
                saveDTO.CreationDate = DateTime.Now;
                saveDTO.CreationUser = 1;

                Role role = new Role()
                {

                    Rolename = saveDTO.Rolename,
                    CreationDate = saveDTO.CreationDate,
                    DeletedUser = saveDTO.CreationUser,
                    Description = saveDTO.Description,
                    CreationUser = saveDTO.CreationUser,

                }; 

                var result = await this.roleRepository.Save(role);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el rol";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {

            var result = await this.roleRepository.GetRolesById(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el rol
            }
            return View(result.Result);

        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleUpdateDTO updateDTO)
        {
            try
            {
                updateDTO.ModifyDate = DateTime.Now;
                updateDTO.ModifyUser = 1;

                Role role = new Role()
                {

                    Id = updateDTO.Id,
                    Rolename = updateDTO.Rolename,
                    ModifyUser = updateDTO.ModifyUser,
                    ModifyDate = updateDTO.ModifyDate,
                    Description = updateDTO.Description,
                    CreationUser = updateDTO.CreationUser,



                };

                var result = await this.roleRepository.Update(role);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error actualizando el rol";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

    }
}
