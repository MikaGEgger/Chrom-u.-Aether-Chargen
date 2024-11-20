using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class AttributeModifier
    {
        public AttributeModifier()
        {
        }

        public AttributeModifier(CharacterAttribute? characterAttribute, int modifier)
        {
            this.Attribute = characterAttribute;
            this.Modifier = modifier;
        }

        public CharacterAttribute Attribute { get; set; }
        public int Modifier { get; set; }
    }
}