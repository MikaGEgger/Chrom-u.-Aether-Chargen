using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class CharacterOrigin
    {
        public string Name { get; set; }
        public string Description { get; set;}

        public List<CharacterAttribute> AttributeBoni { get; set; }
        public List<CharacterAttribute> AttributeMali { get; set; }

    }
}