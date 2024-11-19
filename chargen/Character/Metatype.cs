using System.Reflection.Metadata.Ecma335;
using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class Metatype
    {
      public string Name { get; set; }

     public List<Tuple<int, CharacterAttribute>> AttributeModifiers { get; set; }

    public Tuple<int, int> RollRange { get; set; }

    public List<String> RaceSpecialities { get; set; }
    
    public override string ToString()
    {
      return Name;
    }
    }
    
}