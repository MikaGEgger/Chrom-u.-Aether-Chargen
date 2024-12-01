using chargen.Character.Career;
using chargen.Character.CharacterProperties;
using chargen.Character;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp;
using GalaSoft.MvvmLight.Command;
using chargen.RulesetConstants;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for CarreerView.xaml
    /// </summary>
    public partial class CarreerView : UserControl
    {
        public ObservableCollection<Career> Careers { get; set; }
        private Career selectedCareer;
        private Character_ character;

        public Career SelectedCareer
        {
            get => selectedCareer;
            set
            {
                selectedCareer = value;
                OnPropertyChanged(nameof(SelectedCareer));
                UpdateCareerDetails();
            }
        }

        // Career Details
        public string CareerAttribute { get; set; }
        public string CareerSampleSkills { get; set; }
        public string CareerSampleOccupations { get; set; }
        public string FinancialPotential { get; set; }

        public int CompletedTerms { get; set; }
        public ObservableCollection<CharacterSkill> LearnedSkills { get; set; }
        public int LastRoll { get; set; }
        public string LastRollOutcome { get; set; }

        // Commands
        public ICommand AddTermCommand { get; }
        public ICommand RemoveTermCommand { get; }
        public ICommand RollRiskCommand { get; }
        public ICommand UseFatePointCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarreerView(MainWindow mainWindow, Character_ characterToBeCreated)
        {
            InitializeComponent();

            // Initialize properties
            this.character = characterToBeCreated;
            var rulesetConstants = new RulesetConstants();
            Careers = new ObservableCollection<Career>(rulesetConstants.Careers);
            
            LearnedSkills = new ObservableCollection<CharacterSkill>();

            // Set up commands
            AddTermCommand = new RelayCommand(AddTerm);
            RemoveTermCommand = new RelayCommand(RemoveTerm);
            RollRiskCommand = new RelayCommand(RollRisk);
            UseFatePointCommand = new RelayCommand(UseFatePoint);
            DataContext = this;
        }

      

        private void UpdateCareerDetails()
        {
            if (SelectedCareer != null)
            {
                CareerAttribute = SelectedCareer.CheckAttribute?.ToString() ?? "N/A";
                CareerSampleSkills = string.Join(", ", SelectedCareer.SampleSkills.Select(skill => skill.Name));
                CareerSampleOccupations = string.Join(", ", SelectedCareer.SampleOccupations);
                FinancialPotential = $"{SelectedCareer.DiceCountPerTerm}d{SelectedCareer.DiceTypePerTurn}%, Max: {SelectedCareer.MaximumFinancialPotential}";

                OnPropertyChanged(nameof(CareerAttribute));
                OnPropertyChanged(nameof(CareerSampleSkills));
                OnPropertyChanged(nameof(CareerSampleOccupations));
                OnPropertyChanged(nameof(FinancialPotential));
            }
        }

        private void AddTerm()
        {
            CompletedTerms++;
            OnPropertyChanged(nameof(CompletedTerms));
        }

        private void RemoveTerm()
        {
            if (CompletedTerms > 0)
            {
                CompletedTerms--;
                OnPropertyChanged(nameof(CompletedTerms));
            }
        }

        private void RollRisk()
        {
            var random = new Random();
            LastRoll = random.Next(1, 101);
            // Simulate roll outcome
            var checkAttribute = character.Attributess.FirstOrDefault(attr => attr.AttributeName == SelectedCareer.CheckAttribute.AttributeName);
            if (checkAttribute != null)
            {
                LastRollOutcome = LastRoll <= checkAttribute.ComputedValue
                    ? "No incident"
                    : "Incident occurred!";
            }
            else
            {
                var check = character.Attributess.Max(attr => attr.ComputedValue);
                LastRollOutcome = LastRoll <= check
                ? "No incident"
                : "Incident occurred!";
            }

            OnPropertyChanged(nameof(LastRoll));
            OnPropertyChanged(nameof(LastRollOutcome));
        }

        private void UseFatePoint()
        {
            // Logic to reroll LastRoll
            RollRisk();
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
