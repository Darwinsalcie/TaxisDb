using Microsoft.AspNetCore.Mvc;
using TaxisDb.Domain.Entities;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Web.Models.Taxi;

namespace TaxisDb.Web.Controllers
{
    public class TaxiController : Controller
    {
        private readonly ITaxiRepository taxiRepository;

        public TaxiController(ITaxiRepository taxiRepository)
        {
            this.taxiRepository = taxiRepository;
        }

        // GET: TaxiController
        public async Task<IActionResult> Index()
        {
            var result = await this.taxiRepository.GetTaxis();

            return View(result);
        }

        // GET: TaxiController/Details/5
        public async Task<IActionResult> Details(int Id)
        {

            var result = await this.taxiRepository.GetTaxibyId(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el taxi
            }
            return View(result.Result);

        }

        // GET: TaxiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxiSaveDTO saveDTO)
        {
            try
            {
                saveDTO.CreationDate = DateTime.Now;
                saveDTO.CreationUser = 1;

                Taxi taxi = new Taxi()
                {
                    UserId = saveDTO.UserId,
                    CreationDate = saveDTO.CreationDate,
                    Placa = saveDTO.Placa,
                    Marca = saveDTO.Marca,
                    Año = saveDTO.Año,
                    Modelo = saveDTO.Modelo,
                    Kilometraje = saveDTO.Kilometraje,

                };

                var result = await this.taxiRepository.Save(taxi);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el taxi";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TaxiController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {

            var result = await this.taxiRepository.GetTaxibyId(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el taxi
            }
            return View(result.Result);

        }

        // POST: TaxiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaxiUpdateDTO updateDTO)
        {
            try
            {
                updateDTO.ModifyDate = DateTime.Now;
                updateDTO.ModifyUser = 1;

                Taxi taxi = new Taxi()
                {
                    UserId = updateDTO.UserId,
                    Placa = updateDTO.Placa,
                    Marca = updateDTO.Marca,
                    Año = updateDTO.Año,
                    Modelo = updateDTO.Modelo,
                    Kilometraje = updateDTO.Kilometraje,
                };

                var result = await this.taxiRepository.Update(taxi);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error actualizando el taxi";
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
