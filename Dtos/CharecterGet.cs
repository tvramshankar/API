using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Dtos
{
    public class CharecterGet
    {
    public int Id { get; set; }
    public string Name { get; set; } = "Hai";
    public Charecter person { get; set; } = Charecter.Ram;
    public GetWeaponDto? weapon { get; set; }
    public List<GetSkillDto>? Skills { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int Intelligence { get; set; }
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
    }
}