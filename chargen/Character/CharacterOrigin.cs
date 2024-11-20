using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class CharacterOrigin
    {

               public string Name { get; set; }
        
        public List<CharacterAttribute> AttributeBoni { get; set; }
        public List<CharacterAttribute> AttributeMali { get; set; }
        
        public int FatePointBonus { get; set; }
       
        public int BaseJobAge { get; set; }

       
        public string SampleJobs { get; set;}
        public int DiceRangeMin { get; internal set; }
        public int DiceRangeMax { get; internal set; }
        public int FatePointsDiceNumber { get; internal set; }
        public int FatePointsDiceType { get; internal set; }
        public int FatePointsDiceBonus { get; internal set; }
        public int StartAgeBase { get; internal set; }
        public int StartAgeDiceCount { get; internal set; }
        public int StartAgeDiceType { get; internal set; }
    }
}