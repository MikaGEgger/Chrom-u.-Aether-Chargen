using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
namespace chargen.Character.CharacterProperties
{
    public class CharacterSkill : INotifyPropertyChanged
    {
        private string skillName;
        private KnowledgeLevel _knowledgeLevel;

        public event PropertyChangedEventHandler? PropertyChanged;

        public KnowledgeLevel CurrentLevel
        {
            get => _knowledgeLevel;
            set
            {
                if (_knowledgeLevel != value)
                {
                    _knowledgeLevel = value;
                    OnPropertyChanged(nameof(CurrentLevel));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CharacterSkill()
        {
            CurrentLevel = KnowledgeLevel.Untrained;
            Specializations = new List<string>();
        }

        public string Name
        {
            get { return skillName; }
            set { skillName = value; }
        }

        public enum KnowledgeLevel
        {
            Untrained, // Default
            Trained,
            Advanced,
            Expert
        }

        [XmlIgnore]
        public List<string>  Specializations { get; set; }
    }
}