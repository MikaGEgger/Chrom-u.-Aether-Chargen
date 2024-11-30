using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
namespace chargen.Character.CharacterProperties
{
    public class CharacterSkill : INotifyPropertyChanged
    {
        private string skillName;
        private KnowledgeLevels _knowledgeLevel;

        public event PropertyChangedEventHandler? PropertyChanged;

        public KnowledgeLevels KnowledgeLevel
        {
            get => _knowledgeLevel;
            set
            {
                if (_knowledgeLevel != value)
                {
                    _knowledgeLevel = value;
                    OnPropertyChanged(nameof(KnowledgeLevel));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CharacterSkill()
        {
            KnowledgeLevel = KnowledgeLevels.Untrained;
            Specializations = new List<string>();
        }

        public string Name
        {
            get { return skillName; }
            set { skillName = value; }
        }

        public enum KnowledgeLevels
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