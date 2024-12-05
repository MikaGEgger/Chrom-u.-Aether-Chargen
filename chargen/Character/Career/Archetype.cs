using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace chargen.Character.Career
{
    public class Archetype
    {
        public string Name { get; set; }

        [XmlIgnore]
        public string Description { get; set; }
        [XmlIgnore]
        public List<string> Skills { get; set; }

        public Archetype()
        {
            Skills = new List<string>();
        }

        public override string ToString()
        {
            return $"Name: {Name}\nDescription: {Description}\nSkills: {string.Join(", ", Skills)}";
        }
    }

   
}
