using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Dtos
{
    public class AddWeaponsDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int CharecterId { get; set; }
    }
}