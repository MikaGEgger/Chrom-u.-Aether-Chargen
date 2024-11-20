using chargen.Character.CharacterProperties;
using System.Xml.Serialization;

namespace chargen.Character.Career
{
    public class Career
    {
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlIgnore]
        public CharacterAttribute CheckAttribute { get; set; }

        [XmlIgnore]
        public List<string> SampleOccupations { get; set; }

        [XmlIgnore]
        public List<CharacterSkill> SampleSkills { get; set; }

        [XmlIgnore]
        public int MaximumFinancialPotential { get; set; }

        [XmlIgnore]
        public int DiceCountPerTerm { get; internal set; }

        [XmlIgnore]
        public int DiceTypePerTurn { get; internal set; }

        public Career()
        {
            this.CheckAttribute = new CharacterAttribute();
            this.SampleOccupations = new List<string>();
            this.SampleSkills = new List<CharacterSkill>();
        }

    }
}