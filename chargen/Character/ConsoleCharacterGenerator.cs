using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chargen.Character
{
    public static class ConsoleCharacterGenerator
    {
        
        public static Character CreateCharacter(RulesetConstants.RulesetConstants constants)
        {           
           
            Console.WriteLine("\n\r =================================================================\n\r\n\r\n\r\n\r");
            Console.WriteLine("Enter Character Name:");
            string characterName = Console.ReadLine();
            Metatype metatype = SetMetaType(constants.MetaTypes);
            CharacterOrigin origin= SetCharacterOrigin(constants.CharacterOrigins);
            return new Character(characterName, metatype, origin);
        }

        private static CharacterOrigin SetCharacterOrigin(List<CharacterOrigin> characterOrigins)
        {
            string originNames = "";
            for (int i = 0; i < characterOrigins.Count; i++)
            {
                int j = i + 1;
                originNames = originNames + j + " - " + characterOrigins[i].Name + ", ";
            }
            Console.WriteLine("Chose Your Character Origin: [" + originNames + " 0 - Random]");
            int originInput = 0;
            int originValue = 0;

            while (!(int.TryParse(Console.ReadLine(), out originInput) && (originInput >= 0 && originInput <= 4)))
            {
                Console.Write("Invalid Input Parameter. Valid Parameters: [" + originNames + " 0 - Random]");
            }
            if (originInput == 0)
            {
                originValue = Random.Shared.Next(1, 100);
            }
            else
            {
                originValue = characterOrigins[originInput - 1].RollRange.Item1 + 1;
            }
            CharacterOrigin characterOrigin = characterOrigins.FirstOrDefault(x => x.RollRange.Item1 <= originValue && x.RollRange.Item2 >= originValue);
            return characterOrigin;
        }

        private static Metatype SetMetaType(List<Metatype> metatypes)
        {
            string metatypeNames = "";
            for (int i = 0; i < metatypes.Count; i++)
            {
                int j = i + 1;
                metatypeNames = metatypeNames + j + " - " + metatypes[i].Name + ", ";
            }
            Console.WriteLine("Chose Your Origin: [" + metatypes + " 0 - Random]");
            int metatypeInput = 0;
            int metatypeValue = 0;

            while (!(int.TryParse(Console.ReadLine(), out metatypeInput) && (metatypeInput >= 0 && metatypeInput <= 5)))
            {
                Console.Write("Invalid Input Parameter. Valid Parameters: [" + metatypeNames + " 0 - Random]");
            }
            if (metatypeInput == 0)
            {
                metatypeValue = Random.Shared.Next(1, 100);
            }
            else
            {
                metatypeValue = metatypes[metatypeInput - 1].RollRange.Item1 + 1;
            }
            Metatype metatype = metatypes.FirstOrDefault(x => x.RollRange.Item1 <= metatypeValue && x.RollRange.Item2 >= metatypeValue);
            return metatype;
        }
    }
}