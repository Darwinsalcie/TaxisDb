using Microsoft.AspNetCore.Mvc;
using TaxisDb.API.Models.Role;
using TaxisDb.API.Models.User;
using TaxisDb.Domain.Entities;
using TaxisDb.Domain.Models;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Models.Role;
using TaxisDb.Persistence.Models.User;
using TaxisDb.Persistence.Models.UserGroup;
using TaxisDb.Persistence.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxisDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        // GET: api/<UserController>
        [HttpGet("GetUser")]
        public async Task<IActionResult> Get()
        {
            DataResults<List<UserModel>> result = new DataResults<List<UserModel>>();


            result = await this.userRepository.GetUser();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GetRolesByUserGroupId")]
        public async Task<IActionResult> GetByUserGroupId(int usergroupId)
        {
            DataResults<List<UserModel>> result = new DataResults<List<UserModel>>();


            result = await this.userRepository.GetUserByUserGroup(usergroupId);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserByName")]
        public async Task<IActionResult> Get(string name)
        {
            DataResults<UserModel> result = new DataResults<UserModel>();

            result = await this.userRepository.GetUserbyName(name);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // POST api/<UserController>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> Post([FromBody] UserSaveDTO userSaveDTO)
        {
            bool result = false;

            result = await this.userRepository.Save(new Domain.Entities.User()
            {
                Documento = userSaveDTO.Documento,
                UserGroupId = userSaveDTO.UserGroupId,
                UserGroupReqId = userSaveDTO.UserGroupReqId,
                Nombre = userSaveDTO.Nombre,
                Apellido = userSaveDTO.Apellido,
                CreationDate = userSaveDTO.CreationDate,
                CreationUser = userSaveDTO.CreationUser,
            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }


        // PUT api/<UserController>/5
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> Put([FromBody] UserUpdateDTO userUpdateDTO)
        {
            bool result = false;

            result = await this.userRepository.Update(new Domain.Entities.User()
            {
                Documento = userUpdateDTO.Documento,
                UserGroupId = userUpdateDTO.UserGroupId,
                UserGroupReqId = userUpdateDTO.UserGroupReqId,
                Nombre = userUpdateDTO.Nombre,
                Apellido = userUpdateDTO.Apellido,
                CreationUser = userUpdateDTO.CreationUser,
                ModifyDate = userUpdateDTO.ModifyDate,
                ModifyUser = userUpdateDTO.ModifyUser,

            });

            if (!result)
            {
                return BadRequest();
            }

            return Ok();

        }


        // DELETE api/<UserController>/5
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await userRepository.Remove(id);
            if (result)
            {
                return NoContent(); // 204 No Content - El rol se eliminó correctamente
            }
            else
            {
                return NotFound(new { Message = "El usuario no fue encontrado." }); // 404 Not Found
            }
        }
    }
}

