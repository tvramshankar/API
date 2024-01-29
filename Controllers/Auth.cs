using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Data;
using Dev.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public Auth(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponce<int>>> Register(UserRegisterDTO userRegisterDTO)
        {
            var responce = await _authRepository.Register(new Models.User{UserName=userRegisterDTO.UserName}, userRegisterDTO.Password);
            if(!responce.IsSucess)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponce<int>>> Login(UserRegisterDTO userRegisterDTO)
        {
            var responce = await _authRepository.Login(userRegisterDTO.UserName, userRegisterDTO.Password);
            if(!responce.IsSucess)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}