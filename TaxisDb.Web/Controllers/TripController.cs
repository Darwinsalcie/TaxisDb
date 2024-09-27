using Microsoft.AspNetCore.Mvc;
using TaxisDb.Domain.Entities;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Web.Models.Trip;

namespace TaxisDb.Web.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        // GET: TripController
        public async Task<IActionResult> Index()
        {
            var result = await this.tripRepository.GetTrip();

            return View(result);
        }

        // GET: TripController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            var result = await this.tripRepository.GetTripbyId(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el rol
            }
            return View(result.Result);

        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripSaveDTO saveDTO)
        {
            try
            {
                saveDTO.CreationDate = DateTime.Now;
                saveDTO.CreationUser = 1;

                Trip trip = new Trip()
                {
                    Taxi_Id = saveDTO.Taxi_Id,
                    FechaInicio = saveDTO.FechaInicio,
                    FechaFin = saveDTO.FechaFin,    
                    Desde = saveDTO.Desde,
                    Hasta = saveDTO.Hasta,
                    Calificacion = saveDTO.Calificacion,
                    CreationUser = saveDTO.CreationUser,



                };

                var result = await this.tripRepository.Save(trip);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error creando el trip";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TripController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {

            var result = await this.tripRepository.GetTripbyId(Id);
            if (result == null)
            {
                return NotFound(); // Manejo del caso donde no se encuentra el trip
            }
            return View(result.Result);

        }

        // POST: TripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TripUpdateDTO updateDTO)
        {
            try
            {
                updateDTO.ModifyDate = DateTime.Now;
                updateDTO.ModifyUser = 1;

                Trip trip = new Trip()
                {
                    Id = updateDTO.Id,
                    Taxi_Id = updateDTO.Taxi_Id,
                    FechaInicio = updateDTO.FechaInicio,
                    FechaFin = updateDTO.FechaFin,
                    Desde = updateDTO.Desde,
                    Hasta = updateDTO.Hasta,
                    Calificacion = updateDTO.Calificacion,
                    CreationUser = updateDTO.CreationUser,

                };

                var result = await this.tripRepository.Update(trip);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error actualizando el trip";
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
