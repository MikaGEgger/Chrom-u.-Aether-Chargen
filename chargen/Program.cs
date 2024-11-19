using chargen.Character;
using chargen.RulesetConstants;
internal class Program
{
    private static void Main(string[] args)
    {
      Console.Title = "C&Ä CharaGen v. 0.1 ";
        Console.WriteLine("Chrom & Äther Character Generator v 0.1");
        Console.WriteLine("\n\r\n\r\n\r\n\r=================================================================\n\r");
        printTitleCard();

        RulesetConstants constants = new RulesetConstants();
        Character character = ConsoleCharacterGenerator.CreateCharacter(constants);
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