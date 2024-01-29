using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Dtos;

namespace Dev.Service
{
    public interface IWeaponService
    {
        Task<ServiceResponce<CharecterGet>> AddWeapons(AddWeaponsDto addWeaponsDto);

        //Task<ServiceResponce<>>

    }
}