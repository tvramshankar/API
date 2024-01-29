using System.Text.Json.Serialization;
using Dev.Models;

public class Rpg
{
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public Charecter person { get; set; } = Charecter.Ram;
    public User? User { get; set; }
    public Weapon? weapon { get; set; }
    public List<Skill>? Skills { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int Intelligence { get; set; }
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
}
