using chargen.Character.CharacterProperties;

namespace chargen.Character.Career
{
    public class Career
    {
        private string name="";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
                public CharacterAttribute CheckAttribute { get; set; }


        public List<string> SampleOccupations { get; set; }

        public List<CharacterSkill> SampleSkills { get; set; }

        public int MyProperty { get; set; }
        public int MaximumFinancialPotential { get; set; }
        public int DiceCountPerTerm { get; internal set; }
        public int DiceTypePerTurn { get; internal set; }

        public Career() 
        {
            this.CheckAttribute = new CharacterAttribute();
            this.SampleOccupations = new List<string>();
            this.SampleSkills = new List<CharacterSkill>();
        }

    }
}