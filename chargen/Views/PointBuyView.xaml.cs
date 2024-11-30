using chargen.Character;
using chargen.Character.Career;
using chargen.RulesetConstants;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using chargen.Character.CharacterProperties;

namespace WpfApp.Views
{
    public partial class PointBuyView : UserControl
    {
        private MainWindow _mainWindow;
        public Character CharacterToBeCreated { get; set; }

        private static readonly string[] DiceFaces = { "1️⃣", "2️⃣", "3️⃣", "4️⃣", "5️⃣", "6️⃣" };


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

        public int LP => (GetAttributePoints("CharacterToBeCreateStärke") + GetAttributePoints("Widerstand")) / 5;
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
            CharacterToBeCreated.Attributess = new List<CharacterAttribute>(rulesetConstants.CharacterAttributes);
            
            DataContext = this;
        }

        private void DiceButton_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            string[] diceFaces = { "⚀", "⚁", "⚂", "⚃", "⚄", "⚅" };

            // Ensure the button has a RotateTransform for animation
            var transform = ((Button)sender).RenderTransform as RotateTransform;
            if (transform == null)
            {
                transform = new RotateTransform();
                ((Button)sender).RenderTransform = transform;
                ((Button)sender).RenderTransformOrigin = new Point(0.5, 0.5);
            }

            // Create the rotation animation
            var animation = new DoubleAnimation(0, 360, TimeSpan.FromMilliseconds(500))
            {
                RepeatBehavior = new RepeatBehavior(3)
            };

            // Create a Storyboard
            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            // Link the animation to the RotateTransform's Angle property
            Storyboard.SetTarget(animation, ((Button)sender));
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Button.RenderTransform).(RotateTransform.Angle)"));

            // Update the dice face after animation completes
            storyboard.Completed += (s, _) =>
            {
                // Find the StackPanel and TextBox
                var stackPanel = (StackPanel)((Button)sender).Parent;
                var textBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault();

                if (textBox != null)
                {
                    // Set the TextBox value to a random dice face
                    RollValue(sender);
                }
            };

            // Start the animation
            storyboard.Begin();
           
        }

        private void RollValue(object sender)
        {
            var stackPanel = (StackPanel)((Button)sender).Parent;

            // Find the first TextBox in the StackPanel
            var textBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault();

            if (textBox != null)
            {  //3d10+20
                int value = 0;
                for (int i = 0; i < 3; i++)
                {
                    value += Random.Shared.Next(1, 11);
                }
                value += 20;
                textBox.Text = value.ToString();
            }
        }

        private int GetAttributePoints(string attributeName)
        {
            var value = CharacterToBeCreated.Attributess.FirstOrDefault(a => a.AttributeName == attributeName)?.Value ?? 0;
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
            CharacterToBeCreated.TotalAttributesSum=CharacterToBeCreated.Attributess.Sum(attr => attr.Value);
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
            _mainWindow.LoadSkillSelectionView();
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadInitialView();
        }

    }
}