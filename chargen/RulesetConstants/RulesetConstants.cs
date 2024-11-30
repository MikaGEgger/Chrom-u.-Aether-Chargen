using chargen.Character;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using System.Collections.ObjectModel;
using System.Xml;

namespace chargen.RulesetConstants
{
    public class RulesetConstants
    {
        private ObservableCollection<CharacterAttribute> characterAttributes;
        public ObservableCollection<CharacterAttribute> CharacterAttributes
        {
            get { return characterAttributes; }
        }

        private List<CharacterSkill> characterSkills;
        public List<CharacterSkill> CharacterSkills
        {
            get { return characterSkills; }
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

        private List<Archetype> archetypes;

        public List<Archetype> Archetypes
        {
            get { return archetypes; }
            set { archetypes = value; }
        }


        private List<CharacterOrigin> characterOrigins;
        public List<CharacterOrigin> CharacterOrigins
        {
            get { return characterOrigins; }
            set { characterOrigins = value; }
        }


        public RulesetConstants()
        {
            characterAttributes = new ObservableCollection<CharacterAttribute>();
            characterSkills = new List<CharacterSkill>();
            careers = new List<Career>();
            metaTypes = new List<Metatype>();
            characterOrigins = new List<CharacterOrigin>();
            archetypes = new List<Archetype>();

            FillCharacterAttributes();
            FillCharacterSkills();
            FillCharacterCareers();
            FillMetaTypes();
            FillCharacterOrigins();
            FillArchetypes();
        }
        private void FillArchetypes()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Archetypes.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                Archetype currentArchetype = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Archetype":
                                currentArchetype = new Archetype();
                                break;
                            case "Name":
                                if (currentArchetype != null && reader.Read())
                                    currentArchetype.Name = reader.Value;
                                break;
                            case "Description":
                                if (currentArchetype != null && reader.Read())
                                    currentArchetype.Description = reader.Value;
                                break;
                            case "Skills":
                                if (currentArchetype != null && reader.Read())
                                    currentArchetype.Skills = new List<string>(reader.Value.Split(','));
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Archetype")
                    {
                        if (currentArchetype != null)
                        {
                            archetypes.Add(currentArchetype);

                        }
                    }
                }
            }
        }

        private void FillCharacterAttributes()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Attributes.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                CharacterAttribute currentAttribute = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Attribute":
                                currentAttribute = new CharacterAttribute();
                                break;
                            case "AttributeName":
                                if (currentAttribute != null && reader.Read())
                                    currentAttribute.AttributeName = reader.Value;
                                break;
                            case "AttributeCode":
                                if (currentAttribute != null && reader.Read())
                                    currentAttribute.AttributeCode = reader.Value;
                                break;
                            case "AttributeDescription":
                                if (currentAttribute != null && reader.Read())
                                    currentAttribute.Description = reader.Value;
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Attribute")
                    {
                        if (currentAttribute != null)
                        {
                            characterAttributes.Add(currentAttribute);

                        }
                    }
                }
            }
        }

        private void FillCharacterSkills()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\CharacterSkills.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                CharacterSkill currentSkill = null;
                string currentElement = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Skill":
                                currentSkill = new CharacterSkill();
                                break;
                            case "Name":
                                if (currentSkill != null && reader.Read())
                                    currentSkill.Name = reader.Value;
                                break;
                            case "Specialization":
                                if (currentSkill != null && reader.Read())
                                    currentSkill.Specializations.Add(reader.Value);
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Skill")
                    {
                        if (currentSkill != null)
                        {
                            characterSkills.Add(currentSkill);
                        }
                    }
                }
            }
        }

        private void FillCharacterCareers()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Careers.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                Career currentCareer = null;
                string currentElement = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        currentElement = reader.Name;

                        if (reader.Name == "Carreer")
                        {
                            currentCareer = new Career();
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Text && currentCareer != null)
                    {
                        switch (currentElement)
                        {
                            case "Beruf":
                                currentCareer.Name = reader.Value;
                                break;
                            case "Attr.":
                                currentCareer.CheckAttribute = this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == reader.Value);
                                break;
                            case "Fertigkeiten":
                                currentCareer.SampleSkills = CreateSkillList(reader.Value);
                                break;
                            case "DiceCount":
                                currentCareer.DiceCountPerTerm = int.Parse(reader.Value);
                                break;
                            case "DiceType":
                                currentCareer.DiceTypePerTurn = int.Parse(reader.Value);
                                break;
                            case "maxFIN":
                                currentCareer.MaximumFinancialPotential = Convert.ToInt16(reader.Value);
                                break;
                            case "Funktion":
                                currentCareer.SampleOccupations = reader.Value.Split(',').ToList();
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Carreer")
                    {
                        if (currentCareer != null)
                        {
                            careers.Add(currentCareer);

                        }
                    }
                }

            }
        }

        private List<CharacterSkill> CreateSkillList(string skills)
        {
            List<CharacterSkill> skillList = new List<CharacterSkill>();
            var splitskill = skills.Split(',');
            foreach (var skill in splitskill)
            {
                skillList.Add(this.CharacterSkills.FirstOrDefault(x => x.Name == skill.Trim()));
            }
            return skillList;
        }

        private void FillMetaTypes()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Metatypes.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                Metatype currentMetatype = null;
                string currentElement = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        currentElement = reader.Name;

                        if (reader.Name == "Metatype")
                        {
                            currentMetatype = new Metatype();
                            
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Text && currentMetatype != null)
                    {
                        switch (currentElement)
                        {
                            case "Min":
                                currentMetatype.DiceRangeMin = int.Parse(reader.Value);
                                break;
                            case "Max":
                                currentMetatype.DiceRangeMax = int.Parse(reader.Value);
                                break;
                            case "Name":
                                currentMetatype.Name = reader.Value;
                                break;
                            case "SkillAdjustments":
                                currentMetatype.AttributeModifiers = ParseMetatypeAttributeBoni(reader.Value);
                                break;
                            case "Modifiers":
                                currentMetatype.RaceSpecialities = reader.Value.Split(',').ToList();
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Metatype")
                    {
                        if (currentMetatype != null)
                        {
                            metaTypes.Add(currentMetatype);

                        }
                    }
                }
            }
        }

        private void FillCharacterOrigins()
        {
            string loc = AppContext.BaseDirectory + @"RulesetConstants\Data\Origin.xml";
            using (XmlReader reader = XmlReader.Create(loc))
            {
                CharacterOrigin currentOrigin = null;
                string currentElement = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        currentElement = reader.Name;

                        if (reader.Name == "Origin")
                        {
                            currentOrigin = new CharacterOrigin();
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Text && currentOrigin != null)
                    {
                        switch (currentElement)
                        {
                            case "DiceRangeMin":
                                currentOrigin.DiceRangeMin = int.Parse(reader.Value);
                                break;
                            case "diceRangMAx": // Correct the case mismatch
                                currentOrigin.DiceRangeMax = int.Parse(reader.Value);
                                break;
                            case "OriginName":
                                currentOrigin.Name = reader.Value;
                                break;
                            case "BonusAttributes":
                                currentOrigin.AttributeBoni = ParseOriginAttributes(reader.Value);
                                break;
                            case "MalusAttributes":
                                currentOrigin.AttributeMali = ParseOriginAttributes(reader.Value);
                                break;
                            case "DiceNumber":
                                currentOrigin.FatePointsDiceNumber = int.Parse(reader.Value);
                                break;
                            case "DiceType":
                                currentOrigin.FatePointsDiceType = int.Parse(reader.Value);
                                break;
                            case "DiceBonus":
                                currentOrigin.FatePointsDiceBonus = int.Parse(reader.Value);
                                break;
                            case "StartAgeBase":
                                currentOrigin.StartAgeBase = int.Parse(reader.Value);
                                break;
                            case "StartAgeDiceCount":
                                currentOrigin.StartAgeDiceCount = int.Parse(reader.Value);
                                break;
                            case "StartAgeDiceType":
                                currentOrigin.StartAgeDiceType = int.Parse(reader.Value);
                                break;
                            case "SampleJobs":
                                currentOrigin.SampleJobs = reader.Value;
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Origin")
                    {
                        if (currentOrigin != null)
                        {
                            characterOrigins.Add(currentOrigin);

                        }
                    }
                }
            }
        }

        private List<CharacterAttribute> ParseOriginAttributes(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new List<CharacterAttribute>();
            }
            var items = value.Split(',');
            var characterAttributes = new List<CharacterAttribute>();
            foreach (var item in items)
            {
                CharacterAttribute characterAttribute = this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == item);
                if (characterAttribute!=null)
                {
                    characterAttributes.Add(characterAttribute);
                }
                    

            }
            return characterAttributes;
        }

        private List<AttributeModifier> ParseMetatypeAttributeBoni(string attributeModifiers)
        {
            if (String.IsNullOrEmpty(attributeModifiers))
            {
                return new List<AttributeModifier>();
            }
            var itemsTuples = attributeModifiers.Split(',');
            var characterAttributes = new List<AttributeModifier>();
            foreach (var tuple in itemsTuples)
            {
                var values = tuple.Trim().Split(' ');
                int modifier = Convert.ToInt16(values[0].Trim('%'));
                CharacterAttribute characterAttribute = this.CharacterAttributes.FirstOrDefault(a => a.AttributeCode == values[1]);
                characterAttributes.Add(new AttributeModifier(characterAttribute, modifier));
            }
            return characterAttributes;
        }
    }
}