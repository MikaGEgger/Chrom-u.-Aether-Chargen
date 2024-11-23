using chargen.Character;
using System.Windows;

namespace WpfApp
{
    public static class CharacterGenerator
    {
        public static void GenerateRandomCharacter(string name)
        {
           Character c= ConsoleCharacterGenerator.CreateRandomCharacter(new chargen.RulesetConstants.RulesetConstants());
            c.Name = name;
            CharacterPDFParser.ExportCharacter(c);
        }
    }
}