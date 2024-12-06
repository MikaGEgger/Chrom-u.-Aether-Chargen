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
    public partial class CareerView : UserControl, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private CaAeCharacter _characterToBeCreated;

        private ObservableCollection<Career> _careers;
        private Career _selectedCareer;
        private int _completedTerms;
        private int _lastRoll;
        private int _degreesOfFailure;
        private string _lastRollOutcome;
        private bool _canAddTerm = true;
        private bool _canSelectCareer = true;

        public ObservableCollection<Career> Careers
        {
            get => _careers;
            set
            {
                _careers = value;
                OnPropertyChanged(nameof(Careers));
            }
        }

        public Career SelectedCareer
        {
            get => _selectedCareer;
            set
            {
                _selectedCareer = value;
                OnPropertyChanged(nameof(SelectedCareer));
            }
        }

        public int CompletedTerms
        {
            get => _completedTerms;
            set
            {
                _completedTerms = value;
                OnPropertyChanged(nameof(CompletedTerms));
            }
        }

        public int CurrentAge
        {
            get => _characterToBeCreated.Age;
            set
            {
                _characterToBeCreated.Age = value;
                OnPropertyChanged(nameof(CurrentAge));
            }
        }

        public int LastRoll
        {
            get => _lastRoll;
            set
            {
                _lastRoll = value;
                OnPropertyChanged(nameof(LastRoll));
            }
        }

        public int DegreesOfFailure
        {
            get => _degreesOfFailure;
            set
            {
                _degreesOfFailure = value;
                OnPropertyChanged(nameof(DegreesOfFailure));
            }
        }

        public string LastRollOutcome
        {
            get => _lastRollOutcome;
            set
            {
                _lastRollOutcome = value;
                OnPropertyChanged(nameof(LastRollOutcome));
            }
        }

        public bool CanAddTerm
        {
            get => _canAddTerm;
            set
            {
                _canAddTerm = value;
                OnPropertyChanged(nameof(CanAddTerm));
            }
        }

        public bool CanSelectCareer
        {
            get => _canSelectCareer;
            set
            {
                _canSelectCareer = value;
                OnPropertyChanged(nameof(CanSelectCareer));
            }
        }

        public CareerView(MainWindow mainWindow, CaAeCharacter characterToBeCreated)
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
            var constants = new RulesetConstants();
            return new ObservableCollection<Career>(constants.Careers);
        }

        private void AddTermAndRollRisk_Click(object sender, RoutedEventArgs e)
        {
            // Check if a career is selected
            if (SelectedCareer == null)
            {
                MessageBox.Show("Please select a career first.");
                return;
            }

            // Add Term: Increment age and completed terms
            CompletedTerms++;
            CurrentAge += 5;

            // Roll Risk
            var random = new Random();
            LastRoll = random.Next(1, 101);
            var attributeValue = 0;
            
            if (SelectedCareer.CheckAttribute==null)
            {
                attributeValue = _characterToBeCreated.Attributess.Max(x => x.ComputedValue);
                SelectedCareer.CheckAttribute = _characterToBeCreated.Attributess.FirstOrDefault(x => x.ComputedValue == attributeValue);
            }
            else {
                var attributeToCheck = _characterToBeCreated.Attributess.FirstOrDefault(x => x.AttributeCode.Equals(SelectedCareer.CheckAttribute.AttributeCode));

                attributeValue = attributeToCheck.ComputedValue;
           
            }

            DegreesOfFailure = LastRoll / 10 - attributeValue / 10;

            if (LastRoll > attributeValue)
            {
                
                if (DegreesOfFailure > 2)
                {
                    int penalty = RollPenalty(2);
                    _characterToBeCreated.Attributess.FirstOrDefault(x => x.AttributeCode.Equals(SelectedCareer.CheckAttribute.AttributeCode)).Value -= penalty;
                    _characterToBeCreated.Attributess.FirstOrDefault(x => x.AttributeCode.Equals(SelectedCareer.CheckAttribute.AttributeCode)).ComputedValue -= penalty;
                    LastRollOutcome = $"Career ends after {RollYears()} years! Lost {penalty}% attribute.";
                    CanAddTerm = false;
                    CanSelectCareer = false;
                }
                else
                {
                    int penalty = RollPenalty(1);
                    _characterToBeCreated.Attributess.FirstOrDefault(x => x.AttributeCode.Equals(SelectedCareer.CheckAttribute.AttributeCode)).Value -= penalty;
                    _characterToBeCreated.Attributess.FirstOrDefault(x => x.AttributeCode.Equals(SelectedCareer.CheckAttribute.AttributeCode)).ComputedValue -= penalty;

                    LastRollOutcome = $"Minor setback: Lost {penalty}% attribute.";

                }
            }
            else
            {
                LastRollOutcome = "No incident.";
            }
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            CharacterPDFParser.ExportCharacter(_characterToBeCreated);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.LoadSkillUpgradeView(_characterToBeCreated);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
