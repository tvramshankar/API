using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Damage { get; set; }
        public List<Rpg>? Charecters { get; set; }
    }
}