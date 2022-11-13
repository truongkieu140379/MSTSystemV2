using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TutorSearchSystem.Auth_Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.TokenSettings;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthContainer _container;

        public AuthService(IUnitOfWork unitOfWork, IAuthContainer container)
        {
            _unitOfWork = unitOfWork;
            _container = container;
        }

        public async Task<TSTokenSetting> Authenticate(string email)
        {
            Account account = await _unitOfWork.AccountRepository.Get(email);

            if ( account == null)
            {
                return null;
            }

            //
            var tokenKey = _container.SecretKey;
            var key = Encoding.ASCII.GetBytes(tokenKey);
            //
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, account.Email),
                }),
                //expires in 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);
            //
            TSTokenSetting tSSToken = new TSTokenSetting
            {
                Token = generatedToken
            };

            return tSSToken;
        }

    }
}
