using Login_service.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login_service.JWT
{
    public class Jwt
    {
        public static string CreateJWT(UserResponseDTO user)
        {
            var key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["jwt:key"];

            var claims = new List<Claim>()
            {
                new Claim("id", user.id.ToString()!),
                new Claim("name", user.name!.ToString()),
                new Claim("photo", user.photo!.ToString()),
                new Claim("phone", user.phone!.ToString()),
                new Claim("email", user.email!.ToString()),
                new Claim("password", user.password!.ToString()),
                new Claim("confirm", user.confirm.ToString()!),
                new Claim("message", user.message!.ToString())
            };

            var keySig = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var signing = new SigningCredentials(keySig, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: signing,
                expires: DateTime.UtcNow.AddHours(2)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
