using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Xml.Serialization;
using chargen.Character.CharacterProperties;
using iText.Layout.Properties;

namespace chargen.Character
{
    public static class CharacterXMLParser
    {
        public static void ExportCharacter(CaAeCharacter character)
        {
 TextWriter writer = null;
    try
    {
        var serializer = new XmlSerializer(typeof(CaAeCharacter));
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