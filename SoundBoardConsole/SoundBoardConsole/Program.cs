using SoundBoardConsole.Domain.Database.DatabaseTools;
using System;

namespace SoundBoardConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Tazmar's Prototype Soundboard";
                var startup = new Startup(new DBConnect());
                startup.Run();
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to close app.");
            }
            
        }
    }
}
