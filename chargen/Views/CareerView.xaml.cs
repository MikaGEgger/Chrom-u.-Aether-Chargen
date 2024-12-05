using chargen.Character;
using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using chargen.RulesetConstants;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CharGen;

namespace CharGen.Views
{
    public partial class CareerView : UserControl
    {
        private MainWindow _mainWindow;
        private Character_ _characterToBeCreated;

        public ObservableCollection<Career> Careers { get; set; }
        public Career SelectedCareer { get; set; }
        public int CompletedTerms { get; private set; }
        public int CurrentAge { get; private set; }
        public int LastRoll { get; private set; }
        public int DegreesOfFailure { get; private set; }
        public string LastRollOutcome { get; private set; }
        public bool CanAddTerm { get; private set; } = true;
        public bool CanSelectCareer { get; private set; } = true;
        public bool CanUseFatePoint { get; private set; } = false;

        public CareerView(MainWindow mainWindow, Character_ characterToBeCreated)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _characterToBeCreated = characterToBeCreated;
           
            DataContext = this;

            // Initialize properties
            Careers = LoadCareers();
            CurrentAge = 20; // Example starting age
        }

        private ObservableCollection<Career> LoadCareers()
        {
            var rulesetConstants = new chargen.RulesetConstants.RulesetConstants();
            return new ObservableCollection<Career>(rulesetConstants.Careers);
        }

        private void AddTerm_Click(object sender, RoutedEventArgs e)
        {
            CompletedTerms++;
            CurrentAge += 5;
            CanAddTerm = true;
            UpdateUI();
        }

        private void RollRisk_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            LastRoll = random.Next(1, 101);
            var attributeValue = SelectedCareer.CheckAttribute.Value;
            DegreesOfFailure = Math.Abs(LastRoll / 10 - attributeValue / 10);

            if (LastRoll > attributeValue)
            {
                if (DegreesOfFailure > 2)
                {
                    int penalty = RollPenalty(2);
                    LastRollOutcome = $"Career ends after {RollYears()} years! Lost {penalty}% attribute.";
                    CanAddTerm = false;
                    CanSelectCareer = false;
                }
                else
                {
                    int penalty = RollPenalty(1);
                    LastRollOutcome = $"Minor setback: Lost {penalty}% attribute.";
                }
            }
            else
            {
                LastRollOutcome = "No incident.";
            }

            UpdateUI();
        }

        private void UseFatePoint_Click(object sender, RoutedEventArgs e)
        {
            RollRisk_Click(sender, e);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Career details submitted successfully.");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.LoadSkillUpgradeView(_characterToBeCreated);
        }

        private int RollPenalty(int multiplier)
        {
            var random = new Random();
            int penalty = 0;
            for (int i = 0; i < multiplier; i++)
            {
                penalty += random.Next(1, 6); // Random value 1d5
            }
            return penalty;
        }

        private int RollYears()
        {
            var random = new Random();
            return random.Next(1, 6); // Random value 1d5
        }

        private void UpdateUI()
        {
            OnPropertyChanged(nameof(CompletedTerms));
            OnPropertyChanged(nameof(CurrentAge));
            OnPropertyChanged(nameof(LastRoll));
            OnPropertyChanged(nameof(DegreesOfFailure));
            OnPropertyChanged(nameof(LastRollOutcome));
            OnPropertyChanged(nameof(CanAddTerm));
            OnPropertyChanged(nameof(CanSelectCareer));
            OnPropertyChanged(nameof(SelectedCareer));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
