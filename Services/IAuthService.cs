﻿using System.Threading.Tasks;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public interface IAuthService
    {
        string Login(string login, string password);
        Task<string> RegisterAsync(string login, string password);

        Task<User> GetUser(string login);
    }
}