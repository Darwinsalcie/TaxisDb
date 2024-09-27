using Microsoft.AspNetCore.Mvc;
using TaxisDb.API.Models.Taxi;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Taxi;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxisDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly ITaxiRepository taxiRepository;

        public TaxiController(ITaxiRepository taxiRepository)
        {
            this.taxiRepository = taxiRepository;
        }



        [HttpGet("GetTaxi")]
        public async Task<IActionResult> Get()
        {
            DataResults<List<TaxiModel>> result = new DataResults<List<TaxiModel>>();


            result = await this.taxiRepository.GetTaxis();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GettaxibyuserId")]
        public async Task<IActionResult> Get(int userId)
        {
            DataResults<List<TaxiModel>> result = new DataResults<List<TaxiModel>>();

            result = await this.taxiRepository.GetTaxibyUserId(userId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }



        [HttpGet("GetTaxibyPlaca")]
        public async Task<IActionResult> Get(string placa)
            {
                DataResults<TaxiModel> result = new DataResults<TaxiModel>();

                result = await this.taxiRepository.GetTaxibyPlaca(placa);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);

            }



        // POST api/<TaxiController>
        [HttpPost("CreateTaxi")]
        public async Task<IActionResult> Post([FromBody] TaxiSaveDTO taxiSaveDTO)
        {
            bool result = false;

            result = await this.taxiRepository.Save(new Domain.Entities.Taxi()
            {
                UserId = taxiSaveDTO.UserId,
                Placa = taxiSaveDTO.Placa,
                Marca = taxiSaveDTO.Marca,
                Año = taxiSaveDTO.Año,
                Modelo = taxiSaveDTO.Modelo,
                Kilometraje = taxiSaveDTO.Kilometraje,
                CreationDate = taxiSaveDTO.CreationDate,
                CreationUser = taxiSaveDTO.CreationUser,

            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT api/<TaxiController>/5
        [HttpPut("UpdateTaxi")]
        public async Task<IActionResult> Put([FromBody] TaxiUpdateDTO taxiUpdateDTO)
        {
            bool result = false;

            result = await this.taxiRepository.Update(new Domain.Entities.Taxi()
            {
                UserId = taxiUpdateDTO.UserId,
                Placa = taxiUpdateDTO.Placa,
                Marca = taxiUpdateDTO.Marca,
                Año = taxiUpdateDTO.Año,
                Modelo = taxiUpdateDTO.Modelo,
                Kilometraje = taxiUpdateDTO.Kilometraje,
                CreationUser = taxiUpdateDTO.CreationUser,


            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();

        }

        // DELETE api/<TaxiController>/5
        [HttpDelete("RemoveTaxi")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await taxiRepository.Remove(id);
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
