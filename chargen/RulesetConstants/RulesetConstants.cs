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
                attribute.AttributeCode = fields[0].Split('(')[1].Split(')')[0];
                attribute.Description = fields[1];
                this.CharacterAttributes.Add(attribute);
                Console.WriteLine(attribute.ToString());
            }
        }
        

        private void FillCharacterSkills()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\CharacterSkills.csv";
        
           TextFieldParser parser = new TextFieldParser(loc);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                CharacterSkill skill = new CharacterSkill();
                if(fields.Length !=2)
                {
                    continue;
                }
                skill.SkillName = fields[0];
                skill.Description = fields[1];
                this.CharacterSkills.Add(skill);
                Console.WriteLine(skill.ToString());
            }
        }

        private void FillCharacterCareers()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Careers.csv";
        
              TextFieldParser parser = new TextFieldParser(loc);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                Career career = new Career();
                if(fields.Length !=6)
                {
                    continue;
                }
                career.Name = fields[0];
                career.CheckAttribute = this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == fields[1]);
                career.SampleSkills= CreateSkillList(fields[2]); 
                career.FinancialPotentialperTerm = new Tuple<int,int> (Convert.ToInt16(fields[3].Split('d')[0]),Convert.ToInt16(fields[3].Split('d')[1].TrimEnd('%')));
                career.MaximumFinancialPotential= Convert.ToInt16(fields[4]);
                career.SampleOccupations= fields[5].Split(',').ToList();
                this.Careers.Add(career);
                Console.WriteLine(career.ToString());
            }
        }
        private List<CharacterSkill> CreateSkillList(string skills)
        {
            List<CharacterSkill> skillList = new List<CharacterSkill>();
            var splitskill = skills.Split(',');
            foreach (var skill in splitskill)
            {
                skillList.Add(this.CharacterSkills.FirstOrDefault(x => x.SkillName == skill.Trim()));
            }
            return skillList;
        }
    }
}