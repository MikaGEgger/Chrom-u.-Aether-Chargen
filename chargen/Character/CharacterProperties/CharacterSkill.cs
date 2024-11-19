namespace chargen.Character.CharacterProperties
{
    public class CharacterSkill
    {
        private string skillName;
        public string SkillName
        {
            get { return skillName; }
            set { skillName = value; }
        }

        public string Description { get; set; }
    }
}