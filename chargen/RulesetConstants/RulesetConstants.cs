using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using Microsoft.VisualBasic.FileIO;

namespace chargen.RulesetConstants
{
    public class RulesetConstants
    {
        private List<CharacterAttribute> characterAttributes;
        public List<CharacterAttribute> CharacterAttributes
        {
            get { return characterAttributes; }
        }

        private List<CharacterSkill> chracterSkills;
        public List<CharacterSkill> CharacterSkills
        {
            get { return chracterSkills; }
        }

        private List<Career> careers;
        public List<Career> Careers
        {
            get { return careers; }
        }

        public RulesetConstants()
        {
            characterAttributes = new List<CharacterAttribute>();
            chracterSkills = new List<CharacterSkill>();
            careers = new List<Career>();

            FillCharacterAttributes();
            FillCharacterSkills();
            FillCharacterCareers();
            //!!!!ToDo: move file paths!!!!
        }

        private void FillCharacterAttributes()
        {

            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Attributes.csv";
        
           TextFieldParser parser = new TextFieldParser(loc);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                CharacterAttribute attribute = new CharacterAttribute();
                if(fields.Length !=2)
                {
                    continue;
                }
                attribute.AttributeName = fields[0];
                attribute.Description = fields[1];
                this.characterAttributes.Add(attribute);
                Console.WriteLine(attribute.ToString());
            }
        }
        

        private void FillCharacterSkills()
        {

        }

        private void FillCharacterCareers()
        {

        }

    }
}