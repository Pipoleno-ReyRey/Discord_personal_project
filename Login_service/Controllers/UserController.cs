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
            string jwt = Jwt.CreateJWT(user);
            if (user.id != null)
            {
                return Ok(jwt);
            }
            else
            {
                return BadRequest(jwt);
            }
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var data = await userServices.Post(user);
            string jwt = Jwt.CreateJWT(data);
            if (data.id != null)
            {
                return Ok(jwt);
            }
            else
            {
                return BadRequest(jwt);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            var data = await userServices.Update(user);
            string jwt = Jwt.CreateJWT(data);
            if(data.id != null)
            {
                return Ok(jwt);
            }
            else
            {
                return BadRequest(jwt);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await userServices.Delete(id);
            string jwt = Jwt.CreateJWT(data);
            if (data.id != null)
            {
                return Ok(jwt);
            }
            else
            {
                return BadRequest(jwt);
            }
        }
    }
}
