using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Models;

namespace Dev.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponce<int>> Register(User user, string password);
        Task<ServiceResponce<string>> Login(string username, string password);
        Task<bool> IsUserExists(string UserName);
    }
}