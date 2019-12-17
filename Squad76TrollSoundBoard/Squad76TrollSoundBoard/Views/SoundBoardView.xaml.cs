using Squad76TrollSoundBoard.ViewModels;
using System.Windows;
using Ninject;

namespace Squad76TrollSoundBoard.Views
{
    public partial class SoundBoardView : Window
    {
        public SoundBoardView()
        {
            InitializeComponent();
            DataContext = new SoundBoardViewModel();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var d = (SoundBoardViewModel)DataContext;

            //MessageBox.Show(d.ToString());

            //displays the key being pressed.
            //MessageBox.Show(e.Key.ToString());
        }
    }
}
