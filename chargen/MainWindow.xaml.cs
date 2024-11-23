using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using chargen.Character;
using chargen.RulesetConstants;

namespace MyNewApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

private void DoStuff()
{
    Console.Title = "C&Ä CharaGen v. 0.1 ";
        Console.WriteLine("Chrom & Äther Character Generator v 0.1");
        Console.WriteLine("\n\r\n\r\n\r\n\r=================================================================\n\r");
        printTitleCard();

        RulesetConstants constants = new RulesetConstants();
        Character character = ConsoleCharacterGenerator.CreateCharacter(constants);
        CharacterXMLParser.ExportCharacter(character);
        CharacterPDFParser.ExportCharacter(character);
        Console.WriteLine("Created Character: " + character.ToString());

      }
  

   

    private static void printTitleCard()
    {
      string line;  
    StreamReader sr = new StreamReader(AppContext.BaseDirectory + @"RulesetConstants\Data\TitleCard.Txt");
    line = sr.ReadLine();
    while (line != null)
    {
        Console.WriteLine(line);
        line = sr.ReadLine();
    }
    sr.Close();
   
    }

}