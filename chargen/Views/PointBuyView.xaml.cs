using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Views
{
    public partial class PointBuyView : UserControl
    {
        private MainWindow _mainWindow;

        public PointBuyView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Points distributed successfully.", "Point Buy");
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadInitialView();
        }
    }
}