using Microsoft.AspNetCore.Mvc;
using Servers_service.Models;
using Servers_service.Services;
using Servers_service.DTO;
using Microsoft.AspNetCore.Authorization;
using Servers_service.JWT;

namespace Servers_service.Controllers
{
    [ApiController]
    [Route("servers")]
    public class ServerController : ControllerBase
    {
        private readonly ServerServices services = new ServerServices();

        [Authorize]
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] ServerDTO serverDTO)
        {
            serverDTO.creator = User.FindFirst("name")!.Value;
            var result = await services.Post(serverDTO, int.Parse(User.FindFirst("id")!.Value));
            if(result is Server)
            {
                string jwt = Jwt.ServerJwt(result);
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize]
        [HttpGet("server/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await services.Get(name);
            if(result is Server)
            {
                var jwt = Jwt.ServerJwt(result);
                return Ok(jwt);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize]
        [HttpDelete("delete/{server}")]
        public async Task<IActionResult> Delete(string server)
        {
            var result = await services.Delete(int.Parse(User.FindFirst("id")!.Value), server);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [Authorize]
        [HttpPost("postUser/{server}")]
        public async Task<IActionResult> PostUserServer(string server)
        {
            var result = await services.AddUserServer(int.Parse(User.FindFirst("id")!.Value), User.FindFirst("name")!.Value, server);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [Authorize]
        [HttpDelete("deleteUser/{server}")]
        public async Task<IActionResult> DeleteUserServer(string server)
        {
            var result = await services.DeleteUserServer(int.Parse(User.FindFirst("id")!.Value), User.FindFirst("name")!.Value, server);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
