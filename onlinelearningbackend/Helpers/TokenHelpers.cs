using Microsoft.IdentityModel.Tokens;
using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace onlinelearningbackend
{
    public static class TokenHelpers
    {
        public static string CreateToken(MyUserModel User,byte[] key)
        {
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId",User.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
            var Token = TokenHandler.WriteToken(SecurityToken);
            return Token;
        }
    }
}
