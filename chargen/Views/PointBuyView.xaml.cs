using chargen.Character;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Views
{
    public partial class PointBuyView : UserControl
    {
        private MainWindow _mainWindow;
        public Character Character { get; set; }

        public PointBuyView(MainWindow mainWindow)
        {
            var rulesetConstants = new chargen.RulesetConstants.RulesetConstants();
            InitializeComponent();
            _mainWindow = mainWindow;

            // Load attributes and set data context
            Character = new Character();
            Character.Attributes = rulesetConstants.CharacterAttributes;
            DataContext = Character;
        }

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Process submitted data
            MessageBox.Show($"Character '{Character.Name}' created with allocated points.", "Point Buy");
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadInitialView();
        }

    }
}