namespace chargen.Character
{
    public class Metatype
    {
      public string Name { get; set; }

   public List<Tuple<int, string>> AttributeModifiers { get; set; }
   public List<String> SpecialFeatures { get; set; }

   //ToDo: Add height and Weight values
    }
}