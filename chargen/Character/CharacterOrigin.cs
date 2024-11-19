using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class CharacterOrigin
    {

        public Tuple<int, int> RollRange { get; set; }
        public string Name { get; set; }
        
        public List<CharacterAttribute> AttributeBoni { get; set; }
        public List<CharacterAttribute> AttributeMali { get; set; }
        
        public int FatePointBonus { get; set; }
        public Tuple<int,int> FatePointDice { get; set; }

        public int BaseJobAge { get; set; }

        public Tuple<int,int> JobDice { get; set; }
        public string SampleJobs { get; set;}
    }
}