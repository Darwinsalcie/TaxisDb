using Microsoft.AspNetCore.Mvc;
using TaxisDb.API.Models.Role;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Role;



namespace TaxisDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            DataResults<List<RoleModel>> result = new DataResults<List<RoleModel>>();


            result = await this.roleRepository.GetRoles();

            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<RoleController>/5
        [HttpGet("Get RolebyRoleNmae")]
        public async Task<IActionResult> Get(string rolename)
        {
            DataResults<RoleModel> result = new DataResults<RoleModel>();
            
            result = await this.roleRepository.GetRoles(rolename);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // POST api/<RoleController>
  
        [HttpPost("CreateRole")]
        public async Task<IActionResult> Post([FromBody] RoleSaveDTO roleSaveDTO)
        {
            bool result = false;

            result = await this.roleRepository.Save(new Domain.Entities.Role()
            {
                    Rolename = roleSaveDTO.Rolename,
                    Description = roleSaveDTO.Description,
                    CreationDate = roleSaveDTO.CreationDate,
                    CreationUser = roleSaveDTO.CreationUser,
            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }


        // POST api/<RoleController>/5
        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> Put([FromBody] RoleUpdateDTO roleUpdateDTO)
        {
            bool result = false;

            result = await this.roleRepository.Update(new Domain.Entities.Role()
            {
                Rolename=roleUpdateDTO.Rolename,
                Description = roleUpdateDTO.Description,
                ModifyDate = roleUpdateDTO.ModifyDate,
                ModifyUser = roleUpdateDTO.ModifyUser,
                CreationUser = roleUpdateDTO.CreationUser

            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();

        }

        // DELETE api/<RoleController>/5
        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await roleRepository.Remove(id);
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
