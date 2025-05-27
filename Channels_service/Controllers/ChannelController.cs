using Channels_service.DTO;
using Channels_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Channels_service.Controllers
{
    [ApiController]
    [Route("channel")]
    public class ChannelController : Controller
    {
        private readonly ServicesChannels service = new ServicesChannels();

        [Authorize]
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] ChannelDTO channelDTO)
        {
            var serverId = User.FindFirst("serverId")!.Value;
            var server = User.FindFirst("server")!.Value;
            var result = await service.Post(channelDTO, serverId, server);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var serverId = User.FindFirst("serverId")!.Value;
            var result = await service.GetAll(serverId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("delete/{channel}")]
        public async Task<IActionResult> Delete(string channel)
        {
            var serverId = User.FindFirst("serverId")!.Value;
            var role = User.FindFirst("role")!.Value;
            var result = await service.Delete(channel, serverId, role);
            if (result is string)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }

        }
    }
}
