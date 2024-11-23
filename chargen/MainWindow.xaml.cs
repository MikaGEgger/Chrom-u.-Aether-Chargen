using chargen.RulesetConstants;
using System.Windows;
using WpfApp.Views;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RulesetConstants rulesetConstants = new RulesetConstants();
          
            InitializeComponent();
            LoadInitialView();
        }

        public void LoadInitialView()
        {
            MainContentArea.Content = new InitialView(this);
        }

        public void LoadRollValuesView()
        {
            MainContentArea.Content = new RollValuesView(this);
        }

        public void LoadPointBuyView()
        {
            MainContentArea.Content = new PointBuyView(this);
        }
    }
}