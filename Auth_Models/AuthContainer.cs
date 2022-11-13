using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TutorSearchSystem.Auth_Models
{
    public class AuthContainer : IAuthContainer
    {
        public string SecretKey { get; set; } = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[] Claims { get ; set; }
    }
}
