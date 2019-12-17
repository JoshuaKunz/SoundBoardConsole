using Squad76TrollSoundBoard.Ninject;
using System.Windows;
using Ninject;

namespace Squad76TrollSoundBoard
{
    public partial class App : Application
    {
        public App()
        {

        }

        private void AppStartup(object sender, StartupEventArgs e)
        {
            //MessageBox.Show("STARTING UP");

            //great place to use injection
            IKernel kernel = new StandardKernel(new SoundBoardNinjectModule());
        }
    }
}
