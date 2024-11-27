
using System.ComponentModel;
using System.Xml.Serialization;
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

        public CharacterAttribute()
        {
        }

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        [XmlIgnore]
        public string Description { get; set; }

        public int Value { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChanged { get; internal set; }

        public override string ToString()
        {
            return  attributeName + " - " + Description;
        }    

        public bool Equals(CharacterAttribute other)
        {
            return other.AttributeCode == AttributeCode;
        }

    }
}