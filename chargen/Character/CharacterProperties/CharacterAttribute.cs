namespace chargen.Character.CharacterProperties
{
    public class CharacterAttribute
    {
        private String attributeCode;
        public String AttributeCode
        {
            get { return attributeCode; }
            set { attributeCode = value; }
        }
        

        private string attributeName= "";
        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        public string Description { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            return  attributeName + " - " + Description;
        }    

    }
}