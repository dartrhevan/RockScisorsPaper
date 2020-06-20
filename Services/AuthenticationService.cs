using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public class AuthenticationServiceImpl : IAuthService
    {

        private static string GetToken(string username) =>
            new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username)
                },
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)));


        public string Login(string login, string password)
        {
            using (var db = new AccountDBContext())
            {
                var user = db.Users.Where(u => u.Login == login)
                    .AsEnumerable()
                    .FirstOrDefault(u => BCrypt.Net.BCrypt.Verify(password, u.PasswordHash));
                if (user == null)
                    return null;
                return GetToken(login);
            }
        }

        public async Task<string> RegisterAsync(string login, string password)
        {
            using (var db = new AccountDBContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);
                if (user != null)
                    return null;
                await db.Users.AddAsync(new User(login, password));
                await db.SaveChangesAsync();
                return GetToken(login);
            }
        }

        public async Task<User> GetUser(string login)
        {
            using (var db = new AccountDBContext())
                return await db.Users.FindAsync(login).AsTask();
        }
    }
}