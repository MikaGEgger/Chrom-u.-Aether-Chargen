using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Views
{
    public partial class InitialView : UserControl
    {
        private MainWindow _mainWindow;

        public InitialView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void GenerateCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (RadioRandomCharacter.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(NameInput.Text))
                {
                    NameInput.Visibility = Visibility.Visible;
                    MessageBox.Show("Please enter a name for the character.", "Missing Name");
                    return;
                }

                string name = NameInput.Text;
                CharacterGenerator.GenerateRandomCharacter(name);
            }
            else if (RadioRollValues.IsChecked == true)
            {
                _mainWindow.LoadRollValuesView();
            }
            else if (RadioPointBuy.IsChecked == true)
            {
                _mainWindow.LoadPointBuyView();
            }
            else
            {
                MessageBox.Show("Please select a character generation method.", "Error");
            }
        }
    }
}