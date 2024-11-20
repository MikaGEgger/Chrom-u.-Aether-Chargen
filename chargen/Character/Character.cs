using System.ComponentModel.DataAnnotations;
using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class Character
    {
        public Character()
        {
        }

        public Character(string? characterName, Metatype metatype, CharacterOrigin origin)
        {
            Name = characterName;
            Metatype = metatype;
            Origin = origin;
        }
        #region health

        public int LebenspunkteMax { get; set; }
        public int LebenpunkteCurrent { get; set; }

        public int ErschöpfungspunkteMax { get; set; }
        public int ErschöpfungspunkteCurrent { get; set; }
        #endregion

        public int Movement { get; set; }
        [Range(0, 100)]
        public double Essence { get; set; }
        public Metatype Metatype { get; set; }

        public double Prominence { get; set; }
        public double Finances { get; set; }

        public CharacterOrigin Origin { get; set; }

        public string Name { get; set; }

        public List<CharacterAttribute> Attributes { get; set; }

        public List<CharacterSkill> Skills { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + " Metatype: " + Metatype.ToString();
        }


    }
}