using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dev.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Dev.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<ServiceResponce<int>> Register(User user, string password)
        {
            var responce = new ServiceResponce<int>();
            if(await IsUserExists(user.UserName))
            {
                responce.Message = "User already exists";
                responce.IsSucess = false;
                return responce;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            responce.Data = user.UserId;
            return responce;
        }

        public async Task<ServiceResponce<string>> Login(string username, string password)
        {
            var responce = new ServiceResponce<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(e=>e.UserName.ToLower()== username.ToLower());
            if(user is null)
            {
                responce.IsSucess = false;
                responce.Message = "User not found";
            }
            else if(!ValidateHash(password,user.PasswordHash,user.PasswordSalt))
            {
                responce.IsSucess = false;
                responce.Message="Password Wrong";
            }
            else
            {
                responce.Data = CreateToken(user);
            }
            return responce;
        }

        public async Task<bool> IsUserExists(string userName)
        {
            if(await _dataContext.Users.AnyAsync(e=>e.UserName.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        private bool ValidateHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hash = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var responce = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return responce.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var appSettingsToken = _configuration.GetSection("Appsettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("Appsettings token not found");
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}