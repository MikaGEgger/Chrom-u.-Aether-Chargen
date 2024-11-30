
using chargen.Character.Career;
using System.ComponentModel;
using System.Xml.Serialization;
namespace chargen.Character.CharacterProperties
{
    public class CharacterAttribute : INotifyPropertyChanged
    {
        private String attributeCode;
        public String AttributeCode
        {
            get { return attributeCode; }
            set { attributeCode = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private int setValue;
        public int Value { get => setValue; set
            {
                setValue = value; 
                
            }
        }
       

       


        private int computedValue;
       

        public int ComputedValue
        {
            get { return computedValue; }
            set { computedValue = value; }
        }

        public void CalculateComputedValue(Metatype metatype, CharacterOrigin origin)
        {
            ComputedValue = Value;
            SetMetatypeAdjustments(metatype);
            SetOriginAdjustments(origin);
            OnPropertyChanged("ComputedValue");
        }

        private void SetOriginAdjustments(CharacterOrigin origin)
        {
            if (origin != null)
            {
                if (origin.AttributeBoni != null)
                {
                    foreach (var bonus in origin.AttributeBoni)
                    {
                        if (bonus.Equals(this))
                        {
                            ComputedValue += Random.Shared.Next(1, 11);
                        }
                    }
                }
                if (origin.AttributeMali != null)
                {
                    foreach (var malus in origin.AttributeMali)
                    {
                        if (malus.Equals(this))
                        {
                            ComputedValue -= Random.Shared.Next(1, 11);
                        }
                    }
                }
            }
        }

        private void SetMetatypeAdjustments(Metatype metatype)
        {
            if (metatype != null && metatype.AttributeModifiers != null)
            {
                foreach (var modifier in metatype.AttributeModifiers)
                {
                    if (modifier.Attribute.AttributeCode.Equals(this.AttributeCode))
                    {
                        ComputedValue = Value + modifier.Modifier;

                    }
                }
            }
            else
            {
                ComputedValue = Value;

            }
        }

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