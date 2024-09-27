using Microsoft.AspNetCore.Mvc;
using TaxisDb.Domain.Entities;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Web.Models.User;

namespace TaxisDb.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }


        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var result = await this.userRepository.GetUser();

            return View(result);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            var result = await this.userRepository.GetUserById(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra
            }
            return View(result.Result);

        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserSaveDTO saveDTO)
        {
            try
            {
                saveDTO.CreationDate = DateTime.Now;
                saveDTO.CreationUser = 1;

                User user = new User()
                {
                    Id = saveDTO.Id,
                    UserGroupId = saveDTO.UserGroupId,
                    UserGroupReqId = saveDTO.UserGroupReqId,
                    Documento = saveDTO.Documento,
                    Nombre = saveDTO.Nombre,
                    Apellido = saveDTO.Apellido

                };

                var result = await this.userRepository.Save(user);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el user";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int Id)
        {

            var result = await this.userRepository.GetUserById(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el user
            }
            return View(result.Result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDTO); // En caso de validación fallida
            }

            try
            {
                updateDTO.ModifyDate = DateTime.Now;
                updateDTO.ModifyUser = 1;

                User user = new User()
                {
                    Id = updateDTO.Id, // Asegúrate de que el Id esté presente
                    UserGroupId = updateDTO.UserGroupId,
                    UserGroupReqId = updateDTO.UserGroupReqId,
                    Documento = updateDTO.Documento,
                    Nombre = updateDTO.Nombre,
                    Apellido = updateDTO.Apellido
                };

                var result = await this.userRepository.Update(user);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error actualizando el usuario";
                    return View(updateDTO);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Se produjo un error inesperado.";
                return View(updateDTO);
            }
        }


    }
}
