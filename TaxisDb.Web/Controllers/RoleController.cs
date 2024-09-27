using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Role;

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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {

            var role = await this.roleRepository.GetRolesById(Id);

 

            return View(role.Result);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
