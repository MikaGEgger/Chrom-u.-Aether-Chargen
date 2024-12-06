using Baksteen.Extensions.DeepCopy;
using chargen.Character;
using chargen.Character.CharacterProperties;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static chargen.Character.CharacterProperties.CharacterSkill;

namespace CharGen.Views
{
    public partial class SkillUpgradeView : UserControl
    {
        private const int MaxSkillIncreases = 2;
        private int _currentSkillIncreaseCount = 0;
        private CaAeCharacter _characterToBeCreated;
        private MainWindow _mainWindow;

        public List<CharacterSkill> skills { get; set; }
        public List<CharacterSkill> existingSkills { get; set; }
        public int SkillIncreaseCount { get; private set; }
        public bool CanModifyKnowledgeLevel => SkillIncreaseCount < MaxSkillIncreases;
        public bool ShowValidationMessage => SkillIncreaseCount > MaxSkillIncreases;

       
       

        public SkillUpgradeView(MainWindow mainWindow, CaAeCharacter characterToBeCreated)
        {
            InitializeComponent();
            skills=SetExistingSkills(characterToBeCreated);
            existingSkills = SetExistingSkills(characterToBeCreated);
            _characterToBeCreated = characterToBeCreated;
            _mainWindow = mainWindow;

            DataContext = this;
        }

        private List<CharacterSkill>? SetExistingSkills(CaAeCharacter characterToBeCreated)
        {
           var existint = new ObservableCollection<CharacterSkill>();
            foreach (var skill in characterToBeCreated.Skills)
            {
                var newSkill = new CharacterSkill();
                newSkill.Name = skill.Name;
                newSkill.CurrentLevel = skill.CurrentLevel;
                existint.Add(newSkill);
            }
            return existint.ToList();
        }

        private void UntrainedRadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            var skill = radioButton?.DataContext as CharacterSkill;

            // Disable "Untrained" if the skill is already Trained or Advanced
            if (skill != null && skill.CurrentLevel != KnowledgeLevel.Untrained)
            {
                radioButton.IsEnabled = false;
            }
        }

        private void AdvancedRadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            var skill = radioButton?.DataContext as CharacterSkill;

            // Disable "Advanced" if the skill is Untrained
            if (skill != null && skill.CurrentLevel == KnowledgeLevel.Untrained)
            {
                radioButton.IsEnabled = false;
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            _currentSkillIncreaseCount = 0;
            foreach (var selectedSkill in skills)
            {

                if (selectedSkill == null) return;
                foreach (var existingSkill in existingSkills)
                {
                    if (existingSkill.Name == selectedSkill.Name && existingSkill.CurrentLevel != selectedSkill.CurrentLevel)
                    {
                        _currentSkillIncreaseCount++;
                    }
                }
            }

            var modifiedSkill = (sender as RadioButton)?.DataContext as CharacterSkill;
            if (_currentSkillIncreaseCount > MaxSkillIncreases)
            {
                MessageBox.Show("You can only increase 2 skills.");
                // Revert selection

                modifiedSkill.CurrentLevel = existingSkills.First(s => s.Name == modifiedSkill.Name).CurrentLevel;


            }
            else
            {
                SkillIncreaseCount = _currentSkillIncreaseCount;
                _characterToBeCreated.Skills.FirstOrDefault(x=>x.Name.Equals(modifiedSkill.Name)).CurrentLevel = modifiedSkill.CurrentLevel;
                OnPropertyChanged(nameof(SkillIncreaseCount));
                OnPropertyChanged(nameof(CanModifyKnowledgeLevel));
                OnPropertyChanged(nameof(ShowValidationMessage));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.LoadSkillSelectionView(_characterToBeCreated);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSkillIncreaseCount > MaxSkillIncreases)
            {
                MessageBox.Show("You can only increase 2 skills.");
                return;
            }

            // Proceed with confirmation
            var updatedSkills = existingSkills.Where(skill => skill.CurrentLevel != KnowledgeLevel.Untrained);
          
            _mainWindow.LoadCarreerView(_characterToBeCreated);

        }

        // Notify property changed logic
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
