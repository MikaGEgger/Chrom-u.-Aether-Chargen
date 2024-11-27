using chargen.Character;
using chargen.Character.Career;
using chargen.RulesetConstants;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfApp.Views
{
    public partial class PointBuyView : UserControl
    {
        private MainWindow _mainWindow;
        public Character CharacterToBeCreated { get; set; }

        public List<Archetype> AvailableArchetypes { get
            {
                return rulesetConstants.Archetypes;
            }
            }

        public List<CharacterOrigin> AvailableOrigins { get
            {
                return rulesetConstants.CharacterOrigins;
            }
        }

        public List<Metatype> AvailableMetatypes { get
            {
                return rulesetConstants.MetaTypes;
            }
        }

        public int LP => (GetAttributePoints("Stärke") + GetAttributePoints("Widerstand")) / 5;
        public int EP => (GetAttributePoints("Willenskraft") + GetAttributePoints("Widerstand")) / 5;

        public int Bewegung
        {
            get
            {
                int strength = GetAttributePoints("Stärke");
                int dexterity = GetAttributePoints("Geschicklichkeit");
                int resistance = GetAttributePoints("Widerstand");

                if (strength > resistance && dexterity > resistance)
                    return 9;
                else if (strength > resistance || dexterity > resistance)
                    return 8;
                else
                    return 7;
            }
        }

        RulesetConstants rulesetConstants;
        public PointBuyView(MainWindow mainWindow)
        {
             rulesetConstants = new chargen.RulesetConstants.RulesetConstants();
            InitializeComponent();
            _mainWindow = mainWindow;

            // Load attributes and set data context
            CharacterToBeCreated = new Character();
            CharacterToBeCreated.Attributes = rulesetConstants.CharacterAttributes;
            
            DataContext = this;
        }

        private int GetAttributePoints(string attributeName)
        {
            var value = CharacterToBeCreated.Attributes.FirstOrDefault(a => a.AttributeName == attributeName)?.Value ?? 0;
            return value;
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            int max = 100;

            if (!int.TryParse(((TextBox)sender).Text, out int j) || j < 1 || j > max)
            {
                //delete incoret input
                ((TextBox)sender).Text = "";
            }
            else
            {
                //delete leading zeros
                ((TextBox)sender).Text = j.ToString();
            }
            CharacterToBeCreated.CreateComputedElements();
            CharacterToBeCreated.TotalAttributesSum=CharacterToBeCreated.Attributes.Sum(attr => attr.Value);
        }
        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            int max = 100;

            //do not allow futher incorrect typing
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out int i) && i >= 1 && i <= max);
        }

        

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Process submitted data
            MessageBox.Show($"Character '{CharacterToBeCreated.Name}' created with allocated points.", "Point Buy");
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadInitialView();
        }

    }
}