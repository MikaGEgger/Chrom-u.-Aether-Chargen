using System.Windows.Controls;

namespace WpfApp.Views
{
    public partial class RollValuesView : UserControl
    {
        private MainWindow _mainWindow;

        public RollValuesView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadInitialView();
        }
    }
}