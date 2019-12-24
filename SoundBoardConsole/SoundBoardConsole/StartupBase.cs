using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBoardConsole
{
    public class StartupBase
    {
        public void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void Yellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void Magenta()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
    }
}
