using NAudio.Wave;
using SoundBoardConsole.Domain.Database.DatabaseTools;
using System.Collections.Generic;

namespace SoundBoardConsole
{
    public class SoundBoard
    {
        public readonly DBConnect Connection = new DBConnect();
        public List<Sound> Sounds { get; set; } = new List<Sound>();
        public WaveOutEvent Player { get; set; } = new WaveOutEvent();

        public void Play(string path)
        {
            var reader = new WaveFileReader(path);
            Player.Init(reader);
            Player.Play();
        }
    }
}
