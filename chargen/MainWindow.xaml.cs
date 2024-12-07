using chargen.Character;
using chargen.Character.Career;
using chargen.RulesetConstants;
using chargen.Views;
using System.Windows;
using System.Windows.Media;
using CharGen.Views;

namespace CharGen
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

        public void LoadSkillSelectionView(CaAeCharacter character)
        {
            MainContentArea.Content = new SkillSelectionView(this, character);
        }
        public void LoadSkillUpgradeView(CaAeCharacter character)
        {
            MainContentArea.Content = new SkillUpgradeView(this, character);
        }

        internal void LoadCarreerView(CaAeCharacter character)
        {
            MainContentArea.Content = new CareerView(this, character);
        }

        internal void LoadPointBuyView(CaAeCharacter character)
        {
            MainContentArea.Content = new PointBuyView(this, character);
        }
    }
}