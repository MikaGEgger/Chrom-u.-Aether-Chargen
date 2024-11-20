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
        public int Mana { get; set; }
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

        public void CreateComputedElements()
        {
            LebenspunkteMax = CalculateLebensPunkte();
            LebenpunkteCurrent = LebenspunkteMax;
            ErschöpfungspunkteMax = CalculateErschöpfungspunkte();
            ErschöpfungspunkteCurrent =  ErschöpfungspunkteMax;
            Movement = CalculateMovement();
            Essence = CalculateEssence();
            Mana= CalculateMana();
        }

        private int CalculateMana()
        {
            return 0;
            //Not implemented
        }

        private double CalculateEssence()
        {
             return 100;
            //Not implemented
        }

        private int CalculateMovement()
        {
            int str=Attributes.FirstOrDefault(x=>x.AttributeCode=="STR").Value;
            int ges=Attributes.FirstOrDefault(x=>x.AttributeCode=="GES").Value;
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            if(wid>str&&wid>ges)
            {
                return 7;
            }
            else if(wid<str&&wid<ges)
            {
                return 9;
            }
            return 8;
        }

        private int CalculateErschöpfungspunkte()
        {
            //(WIK+WID)/5
            int wik=Attributes.FirstOrDefault(x=>x.AttributeCode=="WIK").Value;            
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (wik+wid)/5;
        }

        private int CalculateLebensPunkte()
        {
            //(STR+WID)/5
            int str=Attributes.FirstOrDefault(x=>x.AttributeCode=="STR").Value;            
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (str+wid)/5;
        }
    }
}