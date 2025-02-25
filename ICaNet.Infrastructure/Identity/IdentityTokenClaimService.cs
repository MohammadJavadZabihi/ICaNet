using ICaNet.ApplicationCore.Constants;
using ICaNet.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Identity
{
    public class IdentityTokenClaimService : ITokenClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityTokenClaimService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetTokenAsync(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthenticationConstants.JWT_SCRET_KEY);

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new UserNotFoundExeption(userName);

            var rols = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName),
                                           new Claim("UserId", user.Id)};

            foreach (var role in rols)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var toeknDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(toeknDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
