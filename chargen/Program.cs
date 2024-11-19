using chargen.Character;
using chargen.RulesetConstants;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

      RulesetConstants constants = new RulesetConstants();
      Character character = new Character();
      Console.WriteLine("Enter Character Name:");
      character.Name = Console.ReadLine();
    }
}