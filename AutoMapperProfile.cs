using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dev.Dtos;
using Dev.Models;

namespace Dev
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CharecterPost, Rpg>();
            //if we want to specifiy maping for
            // a property from which source then 
            //below can be used
            //.ForPath(dest => dest.WeaponsDto, opt => opt.MapFrom(src => src.weapon));
            //.ForPath(dest => dest.Location.State, opt => opt.MapFrom(src => src.State_name));;
            CreateMap<CharecterGet, Rpg>();
            CreateMap<Rpg, CharecterGet>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Weapon, GetWeaponDto>();
        }
    }
}