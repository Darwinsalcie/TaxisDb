using Microsoft.AspNetCore.Mvc;
using TaxisDb.API.Models.Trip;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Trip;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxisDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }


        // GET: api/<TripController>
        [HttpGet("GetTrip")]
        public async Task<IActionResult> GetAll()
        {
            DataResults<List<TripModel>> result = new DataResults<List<TripModel>>();


            result = await this.tripRepository.GetTrip();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<TripController>/5
        [HttpGet("GetTripByRoleNmae")]
        public async Task<IActionResult> GetById(int id)
        {
            DataResults<TripModel> result = new DataResults<TripModel>();

            result = await this.tripRepository.GetTripbyId(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // POST api/<TripController>
        [HttpGet("GetTripsBytaxiId")]
        public async Task<IActionResult> GetByUserId(int TaxiId)
        {
            DataResults<List<TripModel>> result = new DataResults<List<TripModel>>();

            result = await this.tripRepository.GetTripsbyTaxiId(TaxiId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // PUT api/<TripController>/5
        [HttpPost("CreateTrip")]
        public async Task<IActionResult> Post([FromBody] TripSaveDTO tripSaveDTO)
        {
            bool result = false;

            result = await this.tripRepository.Save(new Domain.Entities.Trip()
            {
                Taxi_Id = tripSaveDTO.Taxi_Id,
                FechaInicio = tripSaveDTO.FechaInicio,
                FechaFin = tripSaveDTO.FechaFin,
                Desde = tripSaveDTO.Desde,
                Hasta = tripSaveDTO.Hasta,
                CreationDate = tripSaveDTO.CreationDate,
            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        // DELETE api/<TripController>/5
        [HttpDelete("UpdateTrip")]
        public async Task<IActionResult> Put([FromBody] TripUpdateDTO tripUpdateDTO)
        {
            bool result = false;

            result = await this.tripRepository.Update(new Domain.Entities.Trip()
            {
                Taxi_Id = tripUpdateDTO.Taxi_Id,
                FechaInicio = tripUpdateDTO.FechaInicio,
                FechaFin = tripUpdateDTO.FechaFin,
                Desde = tripUpdateDTO.Desde,
                Hasta = tripUpdateDTO.Hasta,
                CreationUser = tripUpdateDTO.CreationUser,

            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();

        }

        [HttpDelete("RemoveTrip")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await tripRepository.Remove(id);
            if (result)
            {
                return NoContent(); // 204 No Content - El rol se eliminó correctamente
            }
            else
            {
                return NotFound(new { Message = "El rol no fue encontrado." }); // 404 Not Found
            }
        }


    }
}
