using chargen.Character;
using chargen.Character.Career;
using chargen.RulesetConstants;
using chargen.Views;
using System.Windows;
using System.Windows.Media;
using CharGen.Views;
using System.ComponentModel;
using chargen.Character.CharacterProperties;

namespace CharGen
{
    public partial class MainWindow : Window, INotifyPropertyChanged
{
            private CaAeCharacter _character;

        public CaAeCharacter Character
        {
            get => _character;
            set
            {
                _character = value;
                OnPropertyChanged(nameof(Character));
                OnPropertyChanged(nameof(Character.Name));
            }
        }

        public MainWindow()
        {
            this.FontFamily = new FontFamily("Orbitron");
            RulesetConstants rulesetConstants = new RulesetConstants();
            Character = new CaAeCharacter();

            Character.Attributess = new List<CharacterAttribute>(rulesetConstants.CharacterAttributes);
            Character.Skills = new List<CharacterSkill>(rulesetConstants.CharacterSkills);

            InitializeComponent();
            LoadPointBuyView();
        }

        public void LoadInitialView()
        {
            MainContentArea.Content = new InitialView(this);
        }

        public void LoadRollValuesView()
        {
            MainContentArea.Content = new RollValuesView(this);
        }

        public void LoadPointBuyView()
        {
            MainContentArea.Content = new PointBuyView(this, _character);
        }

        public void LoadSkillSelectionView(CaAeCharacter character)
        {
            this.Character = character;
            MainContentArea.Content = new SkillSelectionView(this, character);
        }
        public void LoadSkillUpgradeView(CaAeCharacter character)
        {
            this.Character = character;
            MainContentArea.Content = new SkillUpgradeView(this, character);
        }

        internal void LoadCarreerView(CaAeCharacter character)
        {
            this.Character = character;
            MainContentArea.Content = new CareerView(this, character);
        }

        internal void LoadPointBuyView(CaAeCharacter character)
        {
            this.Character = character;
            MainContentArea.Content = new PointBuyView(this, character);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}