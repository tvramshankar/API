using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Dtos
{
    public class CharecterPost
    {
    public string Name { get; set; } = "Hai";
    public string SchoolName { get; set; } = "Hauu";
    public Charecter person { get; set; } = Charecter.Ram;
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int Intelligence { get; set; }
    }
}