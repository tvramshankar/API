using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dev.Data;
using Dev.Dtos;
using Dev.Migrations;
using Dev.Models;
using Microsoft.EntityFrameworkCore;

namespace Dev.Service
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponce<CharecterGet>> AddWeapons(AddWeaponsDto addWeaponsDto)
        {
            var responce = new ServiceResponce<CharecterGet>();
            GetUserId(out int userId);
            var charecter = _dataContext.Charecter
                .FirstOrDefault(e=>e.Id == addWeaponsDto.CharecterId && e.User!.UserId == userId);
            
            if(charecter is null)
            {
                responce.Message=$"Charecter with Id {addWeaponsDto.CharecterId} not found";
                responce.IsSucess=false;
            }

            var weapons = new Weapon{
                Name = addWeaponsDto.Name,
                Damage = addWeaponsDto.Damage,
                Charecter = charecter
            };

            _dataContext.Weapons.Add(weapons);
            await _dataContext.SaveChangesAsync();
            responce.Data = _mapper.Map<CharecterGet>(charecter);
            return responce;
        } 
        private void GetUserId(out int UserId)
        {
        bool IsPassable = int.TryParse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier),out UserId);
        if(!IsPassable)
            throw new Exception("UserId cant be found from claims");
        }
    }
}