using Login_service.DTO;
using Login_service.Services;
using Microsoft.AspNetCore.Mvc;
using Login_service.JWT;
using Login_service.Models;

namespace Login_service.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserServices userServices;
        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet("{data}")]
        public async Task<IActionResult> Get(string data)
        {
            var user = await userServices.Get(data);
            if (user is string)
            {
                return BadRequest(user);
            }
            else
            {
                return Ok(Jwt.CreateJWT(user));
            }
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var data = await userServices.Post(user);
            if (data is string)
            {
                return BadRequest(data);
            }
            else
            {
                return Ok(Jwt.CreateJWT(data));
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            var data = await userServices.Update(user);
            if (data is string)
            {
                return BadRequest(data);
            }
            else
            {
                return Ok(Jwt.CreateJWT(data));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await userServices.Delete(id);
            if(data is string)
            {
                return BadRequest(data);
            }
            else
            {
                return Ok(Jwt.CreateJWT(data));
            }
        }
    }
}
