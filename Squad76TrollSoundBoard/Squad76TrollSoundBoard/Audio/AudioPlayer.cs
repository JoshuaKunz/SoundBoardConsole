using System.Media;

namespace Squad76TrollSoundBoard.Audio
{
    public class AudioPlayer : IAudioPlayer
    {
        private SoundPlayer player;

        public AudioPlayer()
        {
            player = new SoundPlayer();
        }

        public void Play(string filePath)
        {
            player.SoundLocation = filePath;
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
