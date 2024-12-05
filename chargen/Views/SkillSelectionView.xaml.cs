using chargen.Character;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using chargen.RulesetConstants;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static chargen.Character.CharacterProperties.CharacterSkill;

namespace CharGen.Views
{
    public partial class SkillSelectionView : UserControl
    {
        private MainWindow _mainWindow;

        public Archetype SelectedArchetype { get; }

        private Character_ CharacterToBeCreated;
        public string TrainedSkill1 { get; set; }
        public string TrainedSkill2 { get; set; }

        public ObservableCollection<string> AvailableSkillsForDropdown2 =>
            new ObservableCollection<string>(
                SelectedArchetype.Skills.Where(skill => skill != TrainedSkill1));

        public SkillSelectionView(MainWindow mainWindow, Character_ character)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            SelectedArchetype = character.Archetype;
            CharacterToBeCreated = character;

            // Initialize data context
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.LoadPointBuyView(CharacterToBeCreated);

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (TrainedSkill1 == null || TrainedSkill2 == null)
            {
                MessageBox.Show("Please select two skills to train.");
                return;
            }

            if (TrainedSkill1 == TrainedSkill2)
            {
                MessageBox.Show("Skill 1 and Skill 2 cannot be the same.");
                return;
            }
            
            CharacterToBeCreated.Skills.FirstOrDefault(x => x.Name.Equals(TrainedSkill1)).CurrentLevel = KnowledgeLevel.Trained;
            CharacterToBeCreated.Skills.FirstOrDefault(x => x.Name.Equals(TrainedSkill2)).CurrentLevel = KnowledgeLevel.Trained;

            // Proceed with confirmation logic
            _mainWindow.LoadSkillUpgradeView(CharacterToBeCreated);
        }
    }
    }
