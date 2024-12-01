using chargen.Character.CharacterProperties;
using Baksteen.Extensions.DeepCopy;
using System;
using chargen.Character.Career;
using System.Collections.ObjectModel;


namespace chargen.Character
{
    public static class ConsoleCharacterGenerator
    {
        
        public static Character_ CreateCharacter(RulesetConstants.RulesetConstants constants)
        {           
           Character_ charac = new Character_();
            Console.WriteLine("\n\r =================================================================\n\r\n\r\n\r\n\r");
            Console.WriteLine("Enter Character Name:");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            charac.Metatype= SetMetaType(constants.MetaTypes);
            charac.Origin= SetCharacterOrigin(constants.CharacterOrigins);
            
            return charac;
        }  
        
          
    
        private static List<CharacterAttribute> SetAttributeValues(List<CharacterAttribute> attributes, List<AttributeModifier>metatypeModifiers)
        {
             Console.WriteLine("Set Character Attribues: 1 - Diceroll, 2 - Pointbuy[NOT IMPLEMENTED], 0 - Random");
           
            int attributeselectionMode = 0;
            

            while (!(int.TryParse(Console.ReadLine(), out attributeselectionMode) && (attributeselectionMode >= 0 && attributeselectionMode <= 2)))
            {
                Console.Write("Invalid Input Parameter. Valid Parameters: [1 - Diceroll, 2 - Pointbuy[NOT IMPLEMENTED], 0 - Random]");
            }
            switch (attributeselectionMode)
            {
                case 0:
                RollCharacterAttributes(attributes, false,metatypeModifiers);
                break;
                case 1:
                RollCharacterAttributes(attributes, true,metatypeModifiers);
                break;
                case 2:
                //PointBuy
                break;
            }
            return attributes;
        }

        private static void RollCharacterAttributes(List<CharacterAttribute> attributes, bool manualMode, List<AttributeModifier> metatypeModifiers)
        {
            Console.WriteLine($"Roll character attributes: {attributes.Count}");
            foreach (CharacterAttribute attribute in attributes)
            {
             
                if(manualMode)
                {
                    Console.WriteLine("Press Enter to Roll for Attribute " + attribute.AttributeName);
                    Console.ReadLine();
                }

                //3d10+20
                int value=0;
                for(int i =0;i<3;i++)
                {
                    value+=Random.Shared.Next(1,11);
                }
                value+=20;
                if(metatypeModifiers!=null)
                {
                AttributeModifier modifier = metatypeModifiers.FirstOrDefault(x=>x.Attribute.Equals(attribute));
                
                if(modifier != null)
                {
                    value+=modifier.Modifier;
                }
                }
                attribute.Value = value;

                if(manualMode)
                {
                    Console.WriteLine(attribute.AttributeName +" = " + attribute.Value);
                }
            }

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
                originValue = characterOrigins[originInput - 1].DiceRangeMin + 1;
            }
            CharacterOrigin characterOrigin = DeepCopyObjectExtensions.DeepCopy(characterOrigins.FirstOrDefault(x => x.DiceRangeMin <= originValue && x.DiceRangeMax >= originValue));
            characterOrigin.FatePointBonus= characterOrigin.FatePointsDiceNumber*(Random.Shared.Next(1, characterOrigin.FatePointsDiceType+1))+characterOrigin.FatePointsDiceBonus;
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
            Console.WriteLine("Chose Your Metatype: [" + metatypeNames + " 0 - Random]");
            int metatypeInput = 0;            

            while (!(int.TryParse(Console.ReadLine(), out metatypeInput) && (metatypeInput >= 0 && metatypeInput <= 5)))
            {
                Console.Write("Invalid Input Parameter. Valid Parameters: [" + metatypeNames + " 0 - Random]");
            }
            Metatype metatype = new Metatype();
            if (metatypeInput == 0)
            {
                int metatypeValue = Random.Shared.Next(1, 100);
              metatype = DeepCopyObjectExtensions.DeepCopy(metatypes.FirstOrDefault(x=>x.DiceRangeMin<=metatypeValue&&x.DiceRangeMax>=metatypeValue));
            }
            else
            {
                metatype = DeepCopyObjectExtensions.DeepCopy(metatypes[metatypeInput - 1]);
            }
            
            
            return metatype;
        }

        internal static Character_ CreateRandomCharacter(RulesetConstants.RulesetConstants constants)
        {
           Character_ charac = new Character_();
            charac.Metatype = Random.Shared.GetItems(constants.MetaTypes.ToArray(), 1)[0];
            charac.Origin = Random.Shared.GetItems(constants.CharacterOrigins.ToArray(), 1)[0];
            charac.Attributess = new List<CharacterAttribute>(constants.CharacterAttributes);
            RollCharacterAttributes(charac.Attributess, false, charac.Metatype.AttributeModifiers);
            charac.CreateComputedElements();
            return charac;
        }
    }
}