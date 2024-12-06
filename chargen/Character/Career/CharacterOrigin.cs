using chargen.Character.CharacterProperties;
using System.Xml.Serialization;

namespace chargen.Character.Career
{
    public class CharacterOrigin
    {
        public CharacterOrigin()
        {
        }
        public string Name { get; set; }

        [XmlIgnore]
        public List<CharacterAttribute> AttributeBoni { get; set; }

        [XmlIgnore]
        public List<CharacterAttribute> AttributeMali { get; set; }

        [XmlIgnore]
        public int FatePointBonus { get; set; }

        [XmlIgnore]
        public int BaseJobAge { get; set; }

        [XmlIgnore]
        public string SampleJobs { get; set; }

        [XmlIgnore]
        public int DiceRangeMin { get; set; }

        [XmlIgnore]
        public int DiceRangeMax { get; set; }

        [XmlIgnore]
        public int FatePointsDiceNumber { get; set; }

        [XmlIgnore]
        public int FatePointsDiceType { get; set; }

        [XmlIgnore]
        public int FatePointsDiceBonus { get; set; }

        [XmlIgnore]
        public int StartAgeBase { get; set; }

        [XmlIgnore]
        public int StartAgeDiceCount { get; set; }

        [XmlIgnore]
        public int StartAgeDiceType { get; set; }
    }
}