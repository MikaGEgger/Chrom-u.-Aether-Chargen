using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chargen.Character;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using Microsoft.VisualBasic.FileIO;

using System.Reflection.Metadata.Ecma335;

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

        private List<Metatype> metaTypes;
        public List<Metatype> MetaTypes
        {
            get { return metaTypes; }
            set { metaTypes = value; }
        }

private List<CharacterOrigin> characterOrigins;
public List<CharacterOrigin> CharacterOrigins
{
    get { return characterOrigins; }
    set { characterOrigins = value; }
}
        

        public RulesetConstants()
        {
            characterAttributes = new List<CharacterAttribute>();
            chracterSkills = new List<CharacterSkill>();
            careers = new List<Career>();
            metaTypes = new List<Metatype>();
            characterOrigins = new List<CharacterOrigin>();


            FillCharacterAttributes();
            FillCharacterSkills();
            FillCharacterCareers();
            FillMetaTypes();
            FillCharacterOrigins();
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
                //Console.WriteLine(attribute.ToString());
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
               // Console.WriteLine(skill.ToString());
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
                //Console.WriteLine(career.ToString());
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

        private void FillMetaTypes()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Metatypes.csv";
        
              TextFieldParser parser = new TextFieldParser(loc);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                Metatype metatype =  new Metatype();
                if(fields.Length !=4)
                {
                    continue;
                }
                List<string> rollRange = fields[0].Split('-').ToList();
                metatype.RollRange = new Tuple<int,int>(Convert.ToInt16(rollRange[0]), Convert.ToInt16(rollRange[1]));
                metatype.Name = fields[1];
                metatype.AttributeModifiers = ParseMetatypeAttributes(fields[2]);
                metatype.RaceSpecialities= fields[3].Split(',').ToList();
                this.metaTypes.Add(metatype);
                //Console.WriteLine(metatype.ToString());
            }
        }

         private void FillCharacterOrigins()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Origin.csv";
        
              TextFieldParser parser = new TextFieldParser(loc);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                CharacterOrigin origin =  new CharacterOrigin();
                if(fields.Length !=7)
                {
                    continue;
                }
                List<string> rollRange = fields[0].Split('-').ToList();
                origin.RollRange = new Tuple<int,int>(Convert.ToInt16(rollRange[0]), Convert.ToInt16(rollRange[1]));
                origin.Name = fields[1];
                origin.AttributeBoni=new List<CharacterAttribute>();
                var bonusValues=fields[2].Trim().Split(',');
                foreach(var bonusValue in bonusValues)
                {
                    origin.AttributeBoni.Add(this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == bonusValue.Trim()));
                }
                origin.AttributeMali=new List<CharacterAttribute>();
                var malusValues=fields[3].Trim().Split(',');
                foreach(var malusValue in malusValues)
                {
                    origin.AttributeMali.Add(this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == malusValue.Trim()));
                }
                var fateValues = fields[4].Trim().Split(' ');
                origin.FatePointDice= new Tuple<int,int>(Convert.ToInt16(fateValues[0]),Convert.ToInt16(fateValues[1].Trim('d')));
                origin.FatePointBonus=Convert.ToInt16(fateValues[2]);
                var jobValues=fields[5].Split('+');
                origin.BaseJobAge= Convert.ToInt16(jobValues[0]);
                var jobDice = jobValues[1].Split('d');
                origin.JobDice=new Tuple<int,int>(Convert.ToInt16(jobDice[0]),Convert.ToInt16(jobDice[1].Trim('J').Trim()));
                origin.SampleJobs= fields[6];
                characterOrigins.Add(origin);
            }
        }

        private List<Tuple<int, CharacterAttribute>> ParseMetatypeAttributes(string attributeModifiers)
        {
            if(String.IsNullOrEmpty(attributeModifiers))
            {
                return new List<Tuple<int, CharacterAttribute>>();
            }
            var itemsTuples= attributeModifiers.Split(',');
            var characterAttributes= new List<Tuple<int,CharacterAttribute>>();
            foreach(var tuple in itemsTuples)
            {
                var values=tuple.Trim().Split(' ');
                int modifier = Convert.ToInt16(values[0].Trim('%'));
                CharacterAttribute  characterAttribute = this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == values[1]);
                characterAttributes.Add(new Tuple<int,CharacterAttribute>(modifier, characterAttribute));
            }
            return characterAttributes;
        }
    }
}