using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chargen.Character
{
    public class CharacterOrigin
    {
        public string Name { get; set; }
        public string Description { get; set;}

        public List<Attribute> AttributeBoni { get; set; }
        public List<Attribute> AttributeMali { get; set; }

    }
}