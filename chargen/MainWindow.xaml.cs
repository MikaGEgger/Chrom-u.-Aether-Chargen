 using chargen.RulesetConstants;

using System.Windows;
using System.Windows.Media;
using WpfApp.Views;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.FontFamily = new FontFamily("Orbitron");
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

        public void LoadSkillSelectionView()
        {
            MainContentArea.Content = new SkillSelectionView(this);
        }
    }
}