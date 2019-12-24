using SoundBoardConsole.Domain.Database.DatabaseTools;
using System;
using System.Threading.Tasks;

namespace SoundBoardConsole
{
    public class Startup : StartupBase
    {
        private readonly DBConnect _connection;
        public SoundBoard sb = new SoundBoard();
        public string Message { get; set; } = "";

        public Startup(DBConnect connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            LoadData();
        }

        public ConsoleKey? LastKeyPressed = null;

        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            while(true)
            {
                Console.Clear();
                Green();
                Console.WriteLine("(1)Start Soundboard");
                Console.WriteLine("(2)Add Sounds");
                Console.WriteLine("(3)Remove Sounds");
                Console.WriteLine("----------------------");

                ConsoleKey? key;

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.D1)
                {
                    StartSoundboard();
                    break;
                }
                else if (key == ConsoleKey.D2)
                {
                    AddSounds();
                    break;
                }
                else if(key == ConsoleKey.D3)
                {
                    RemoveSounds();
                    break;
                }
            }
        }

        public void StartSoundboard()
        {
            while(true)
            {
                Console.Clear();
                Green();
                Console.WriteLine("Listening to keyboard(esc to exit to main menu)");

                foreach(var sound in sb.Sounds)
                {
                    Console.WriteLine($"Name: {sound.Name} Key: {sound.KeyBinding}");
                }

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                    break;

                foreach(var sound in sb.Sounds)
                {
                    if(sound.KeyBinding == key.ToString())
                    {
                        //play audio (async)?
                        try
                        {
                            sb.Play(sound.Path);
                        }
                        catch(Exception e)
                        {
                            Red();
                            Console.WriteLine(e.Message);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
            }
            Console.Clear();
            MainMenu();
        }

        public void AddSounds()
        {
            while(true)
            {
                Console.Clear();
                Yellow();

                var sound = new Sound();

                Console.WriteLine("What is the name of this Sound?");
                var name = Console.ReadLine();
                sound.Name = name;
                Console.WriteLine("Good, now what is the file path? (you can copy paste it in)");
                var path = Console.ReadLine();
                sound.Path = path;

                Console.WriteLine("Finally, press the key you wish to bind this to.");
                var keyBinding = Console.ReadKey().Key.ToString();
                sound.KeyBinding = keyBinding;

                Console.Clear();
                Magenta();
                Console.WriteLine(name + " | " + path + " | " + keyBinding);
                Yellow();
                Console.WriteLine("Is this correct?(y/n)(escape to go back to menu)");

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape) MainMenu();

                if(key == ConsoleKey.Y)
                {
                    //update database.
                    _connection.InsertSound(sound);
                    LoadData();

                    Console.Clear();
                    Yellow();
                    Console.WriteLine("Do you want to add another Sound?(y/n");

                    key = Console.ReadKey().Key;

                    if(key != ConsoleKey.Y)
                    {
                        MainMenu();
                        break;
                    }
                }
            }
        }

        public void RemoveSounds()
        {
            while(true)
            {
                if (sb.Sounds.Count < 1)
                    MainMenu();
                Console.Clear();
                Magenta();
                foreach (var sound in sb.Sounds)
                {
                    Console.WriteLine(sound.Name);
                }

                Yellow();
                Console.WriteLine("------------------------------");
                Console.WriteLine("Which Sound do you wish to remove?(enter nothing to go to menu)");

                var deleteName = Console.ReadLine();

                if (deleteName == "")
                    MainMenu();

                Console.WriteLine($"Are you sure you want to delete {deleteName}?(y/n)");

                var key = Console.ReadKey().Key;

                if(key == ConsoleKey.Y)
                {
                    Console.Clear();
                    //remove from database
                    _connection.DeleteSound(deleteName);
                    LoadData();

                    Console.WriteLine("Would you like to delete another?(y/n)");
                    key = Console.ReadKey().Key;
                    if(key != ConsoleKey.Y)
                    {
                        MainMenu();
                    }
                }
            }
            LoadData();
        }

        public void DisplayMessage()
        {
            if(Message != "")
            {
                Console.WriteLine(Message);
                Message = "";
            }
        }

        public void LoadData()
        {
            sb.Sounds = _connection.GetSounds();
        }
    }
}
