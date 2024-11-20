using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace chargen.Character
{
    public static class CharacterXMLParser
    {
        public static void ExportCharacter(Character character)
        {
 TextWriter writer = null;
    try
    {
        var serializer = new XmlSerializer(typeof(Character));
        writer = new StreamWriter(character.Name+".XML",false);
        serializer.Serialize(writer, character);
    }
    finally
    {
        if (writer != null)
            writer.Close();
    }
        }
    }
}