using System.Xml.Serialization;

namespace chargen.Character
{
  public class Metatype
  {
    public Metatype()
    {
    }
    public string Name { get; set; }

    public List<AttributeModifier> AttributeModifiers { get; set; }

    public List<String> RaceSpecialities { get; set; }
  
   [XmlIgnore]
    public int DiceRangeMin { get; set; }

[XmlIgnore]
    public int DiceRangeMax { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }


}