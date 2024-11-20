using System.IO;
using System.Xml.Serialization;
namespace chargen.Character.CharacterProperties
{
    public class CharacterSkill
    {
        private string skillName;

        public CharacterSkill()
        {
        }

        public string SkillName
        {
            get { return skillName; }
            set { skillName = value; }
        }

          [XmlIgnore]
        public string Description { get; set; }
    }
}