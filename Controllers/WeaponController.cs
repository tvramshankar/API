using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Dtos;
using Dev.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }
        [HttpPost("AddWeapon")]
        public async Task<ActionResult<ServiceResponce<CharecterGet>>> AddWeapon(AddWeaponsDto addWeaponsDto)
        {
            return Ok(await _weaponService.AddWeapons(addWeaponsDto));
        }
    }
}