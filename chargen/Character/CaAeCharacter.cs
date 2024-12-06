using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class CaAeCharacter : INotifyPropertyChanged 
    {
        public CaAeCharacter ()
        {
            attributes = new List<CharacterAttribute> ();            
        }

       

       

        public CaAeCharacter(string characterName, Metatype metatype, CharacterOrigin origin)
        {
            Name = characterName;
            Metatype = metatype;
            Origin = origin;
        }
        #region health
        
        private int _lebenspunkteMax;
        private int erschoepfungspunkteMax;
        private int _movement;
        private int _mana;

        private int totalAttributesSum=0;
        private Metatype metatype;

        private int age=18;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        public int TotalAttributesSum
        {
            get { return totalAttributesSum;  }
            set {
                if (totalAttributesSum != value)
                {
                    totalAttributesSum = value;
                    OnPropertyChanged(nameof(TotalAttributesSum));
                    
                }
            }
        }



        public int LebenspunkteMax
        {
            get => _lebenspunkteMax;
            set
            {
                if (_lebenspunkteMax != value)
                {
                    _lebenspunkteMax = value;
                    OnPropertyChanged(nameof(LebenspunkteMax));
                }
            }
        }

        public int ErschoepfungspunkteMax
        {
            get => erschoepfungspunkteMax;
            set
            {
                if (erschoepfungspunkteMax != value)
                {
                    erschoepfungspunkteMax = value;
                    OnPropertyChanged(nameof(ErschoepfungspunkteMax));
                                    }
            }
        }

        public int Movement
        {
            get => _movement;
            set
            {
                if (_movement != value)
                {
                    _movement = value;
                    OnPropertyChanged(nameof(Movement));
                }
            }
        }

        public int Mana
        {
            get => _mana;
            set
            {
                if (_mana != value)
                {
                    _mana = value;
                    OnPropertyChanged(nameof(Mana));
                }
            }
        }

        public int LebenpunkteCurrent { get; set; }

        public int ErschoepfungspunkteCurrent { get; set; }
        #endregion

        private List<CharacterAttribute> attributes ;

        public List<CharacterAttribute> Attributess
        {
            get { return attributes; }
            set {
                if (attributes != value)
                {
                    attributes = value;
                    OnPropertyChanged(nameof(Attributess));
                }
            }
        }

        public double Essence { get; set; }
        public Metatype Metatype { get => metatype; set
            {
                metatype = value;
                CalculateAttributeValues();
                OnPropertyChanged(nameof(Metatype));
            }
        }

        private void CalculateAttributeValues()
        {
            foreach (var attribute in Attributess)
            {
                attribute.CalculateComputedValue(Metatype, Origin);
            }
        }

        public double Prominence { get; set; }
        public double Finances { get; set; }

        private CharacterOrigin origin;

        public CharacterOrigin Origin
        {
            get { return origin; }
            set { origin = value;
                CalculateAttributeValues();
                OnPropertyChanged(nameof(Origin));
            }
        }


        private Archetype archetype;

        public Archetype Archetype
        {
            get { return archetype; }
            set {
                archetype = value;
                OnPropertyChanged(nameof(Archetype));
            }
        }


        public string Name { get; set; }

      

        public List<CharacterSkill> Skills { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            var metatype = Metatype == null ? "None" : Metatype.Name;
            return "Name: " + Name + " Metatype: " + metatype;
        }

        public void CreateComputedElements()
        {
            LebenspunkteMax = CalculateLebensPunkte();
            LebenpunkteCurrent = LebenspunkteMax;
            ErschoepfungspunkteMax = CalculateErschöpfungspunkte();
            ErschoepfungspunkteCurrent =  ErschoepfungspunkteMax;
            Movement = CalculateMovement();
            Essence = CalculateEssence();
            Mana = CalculateMana();
            CalculateAttributeValues();
        }



        private int CalculateMana()
        {
            return 0;
            //Not implemented
        }

        private double CalculateEssence()
        {
             return 100;
            //Not implemented
        }

        private int CalculateMovement()
        {
            int str=Attributess.FirstOrDefault(x=>x.AttributeCode=="STR").Value;
            int ges= Attributess.FirstOrDefault(x=>x.AttributeCode=="GES").Value;
            int wid= Attributess.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            if(wid>str&&wid>ges)
            {
                return 7;
            }
            else if(wid<str&&wid<ges)
            {
                return 9;
            }
            return 8;
        }

        private int CalculateErschöpfungspunkte()
        {
            //(WIK+WID)/5
            int wik= Attributess.FirstOrDefault(x=>x.AttributeCode=="WIK").Value;            
            int wid= Attributess.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (wik+wid)/5;
        }

        private int CalculateLebensPunkte()
        {
            //(STR+WID)/5
            int str= Attributess.FirstOrDefault(x=>x.AttributeCode=="STR").Value;            
            int wid= Attributess.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (str+wid)/5;
        }
    }
}