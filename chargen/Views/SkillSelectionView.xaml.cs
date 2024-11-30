using chargen.Character.CharacterProperties;
using chargen.RulesetConstants;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Views
{
    public partial class SkillSelectionView : UserControl
    {
        private MainWindow _mainWindow;

        RulesetConstants rulesetConstants;

        public ObservableCollection<CharacterSkill> AvailableSkills { get; set; }

        public SkillSelectionView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            rulesetConstants = new RulesetConstants();

            // Example skills (replace with actual data)
            AvailableSkills = new ObservableCollection<CharacterSkill>(rulesetConstants.CharacterSkills);
            

            // Attach command to add specializations
          

            DataContext = this;
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.LoadPointBuyView();
        }

        private void Confirm_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
