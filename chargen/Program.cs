using chargen.Character;
using chargen.RulesetConstants;
internal class Program
{
    private static void Main(string[] args)
    {
      Console.WriteLine("Chrom & Äther Character Generator v 0.1");
      Console.WriteLine("\n\r\n\r\n\r\n\r=================================================================\n\r");
      printTitleCard();

      RulesetConstants constants = new RulesetConstants();
      Character character = new Character();
      Console.WriteLine("\n\r =================================================================\n\r\n\r\n\r\n\r");
      Console.WriteLine("Enter Character Name:");
      character.Name = Console.ReadLine();
      string metatypes = "";
      for(int i = 0; i < constants.MetaTypes.Count; i++)
      {
        int j = i+1;
        metatypes =metatypes + j + " - "+ constants.MetaTypes[i].Name+ ", ";
      }
      Console.WriteLine("Chose Your Metatype: ["+metatypes+" 0 - Random]");
      int metatypeInput = 0;
      int metatypeValue=0;

      while (!(int.TryParse(Console.ReadLine(), out metatypeInput) && (metatypeInput >= 0 && metatypeInput <= 5)))
      {
        Console.Write("Invalid Input Parameter. Valid Parameters: ["+metatypes+" 0 - Random]");
      }
      if(metatypeInput == 0)
      {
        metatypeValue=Random.Shared.Next(1,100);
      }
      else
      {
        metatypeValue=constants.MetaTypes[metatypeInput-1].RollRange.Item1+1;
      }
      character.Metatype= constants.MetaTypes.FirstOrDefault(x => x.RollRange.Item1<=metatypeValue&&x.RollRange.Item2>=metatypeValue);

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