using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dev.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharecterController : ControllerBase
    {
        private readonly ICharecterService _charecterService;
        public CharecterController(ICharecterService charecterService)
        {
            _charecterService = charecterService;
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponce<CharecterGet>>> GetSingle(int id)
        {
            return Ok(await _charecterService.GetSingle(id));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponce<List<CharecterGet>>>> Get()
        {
            var responce = new ServiceResponce<List<CharecterGet>>();
            return Ok(await _charecterService.GetAll());
        }

        [HttpPost("AddCharecter")]
        public async Task<ActionResult<ServiceResponce<List<CharecterGet>>>> AddCharecter(CharecterPost data)
        {
            return Ok(await _charecterService.AddCharecter(data));
        }

        [HttpPut("UpdateCharecter")]
        public async Task<ActionResult<ServiceResponce<List<CharecterGet>>>> AddCharecter(CharecterGet data)
        {
            var responce = await _charecterService.UpdateCharecter(data);
            if (responce.Data is null)
                return BadRequest(responce);
            return Ok(responce);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponce<List<CharecterGet>>>> DeleteCharecter(int Id)
        {
            var responce = await _charecterService.DeleteCharecter(Id);
            if (responce.Data is null)
                return BadRequest(responce);
            return Ok(responce);
        }

        [HttpPost("AddCharecterSkill")]
        public async Task<ActionResult<ServiceResponce<CharecterGet>>> AddCharecterSkill(AddCharecterSkillDto addCharecterSkillDto)
        {
            return Ok(await _charecterService.AddCharecterSkills(addCharecterSkillDto));
        }
    }
}