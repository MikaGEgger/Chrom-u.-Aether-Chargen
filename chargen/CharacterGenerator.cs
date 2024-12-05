using chargen.Character;
using System.Windows;

namespace CharGen
{
    public static class CharacterGenerator
    {
        public static void GenerateRandomCharacter(string name)
        {
           Character_ c= ConsoleCharacterGenerator.CreateRandomCharacter(new chargen.RulesetConstants.RulesetConstants());
            c.Name = name;
            CharacterPDFParser.ExportCharacter(c);
        }
    }
}