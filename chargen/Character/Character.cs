using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;

namespace chargen.Character
{
    public class Character : INotifyPropertyChanged 
    {
        public Character ()
        {
        }

        public Character(string? characterName, Metatype metatype, CharacterOrigin origin)
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


        public double Essence { get; set; }
        public Metatype Metatype { get => metatype; set
            {
                metatype = value;
                OnPropertyChanged(nameof(Metatype));
            }
        }

        public double Prominence { get; set; }
        public double Finances { get; set; }

        public CharacterOrigin Origin { get; set; }

        public Archetype Archetype{ get; set; }

        public string Name { get; set; }

        public List<CharacterAttribute> Attributes { get; set; }

        public List<CharacterSkill> Skills { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return "Name: " + Name + " Metatype: " + Metatype.ToString();
        }

        public void CreateComputedElements()
        {
            LebenspunkteMax = CalculateLebensPunkte();
            LebenpunkteCurrent = LebenspunkteMax;
            ErschoepfungspunkteMax = CalculateErschöpfungspunkte();
            ErschoepfungspunkteCurrent =  ErschoepfungspunkteMax;
            Movement = CalculateMovement();
            Essence = CalculateEssence();
            Mana= CalculateMana();
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
            int str=Attributes.FirstOrDefault(x=>x.AttributeCode=="STR").Value;
            int ges=Attributes.FirstOrDefault(x=>x.AttributeCode=="GES").Value;
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
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
            int wik=Attributes.FirstOrDefault(x=>x.AttributeCode=="WIK").Value;            
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (wik+wid)/5;
        }

        private int CalculateLebensPunkte()
        {
            //(STR+WID)/5
            int str=Attributes.FirstOrDefault(x=>x.AttributeCode=="STR").Value;            
            int wid=Attributes.FirstOrDefault(x=>x.AttributeCode=="WID").Value;
            return (str+wid)/5;
        }
    }
}