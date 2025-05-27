using Microsoft.IdentityModel.Tokens;
using Servers_service.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servers_service.JWT
{
    public class Jwt
    {
       
        public static string UserServerJwt(int userId, string user, string role, string serverId, string server)
        {
            string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("JWT:key")!;
            var claims = new List<Claim>
            {
                new Claim("id", userId.ToString()),
                new Claim("name", user),
                new Claim("role", role),
                new Claim("serverId", serverId),
                new Claim("server", server)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ServerJwt(Server server)
        {
            string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("JWT:key")!;
            var claims = new List<Claim>
            {
                new Claim("id", server.id!),
                new Claim("name", server.name!),
                new Claim("photo", server.image!),
                new Claim("descripcion", server.description!),
                new Claim("link", server.link!),
                new Claim("creationDate", server.creationDate.ToString()!),
                new Claim("state", server.state.ToString()!),
                new Claim("creator", server.creator!)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
