using AADP.Application.Port.In;
using AADP.Application.Port.Out;
using AADP.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AADP.Application.service
{
    public class LogInServices(IUnitOfWork unitOfWork) : ILogInServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Token> GenerateTokenAsync(UserCredential userCredential)
        {
            var user = await _unitOfWork.Repository<Usuario>().LoginAsync(userCredential.Email, userCredential.Password);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return CreateToken(user);
        }


        private static Token CreateToken(Usuario User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyBytes = Encoding.ASCII.GetBytes("wuhihvwhvqjqjo9r89nlgmnldlofwqsfgtrhnvb3456823fthmglkrnkjbvdksbjhbfjbvprobprjb");

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Email, User.CorreoElectronico),
                new Claim(ClaimTypes.Name, User.NombreUsuario)
            ];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new() { AccessToken = tokenHandler.WriteToken(token) };
        }
    }
}
