using System.ComponentModel;

namespace Squad76TrollSoundBoard.ViewModels
{
    public class SoundViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (value == null)
                    return;

                _path = value;
                OnPropertyChanged("Path");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _groupName;
        public string GroupName
        {
            get => _groupName;
            set
            {
                if (value == null)
                    return;
                _groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        private string _keyBinding;
        public string KeyBinding
        {
            get => _keyBinding;
            set
            {
                if (value == null)
                    return;

                _keyBinding = value;
                OnPropertyChanged("KeyBinding");
            }
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
