using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Squad76TrollSoundBoard.ViewModels
{
    public class SoundGroupViewModel : INotifyPropertyChanged
    {
        public SoundGroupViewModel()
        {
            _ = LoadData();
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

        private string _folderPath;
        public string FolderPath
        {
            get => _folderPath;
            set
            {
                if (value == null)
                    return;

                _folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }

        private List<SoundViewModel> _sounds;
        public List<SoundViewModel> Sounds
        {
            get => _sounds;
            set
            {
                if (value == null)
                    return;

                _sounds = value;
                OnPropertyChanged("Sounds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsLoading { get; set; }

        public async Task LoadData()
        {
            try
            {
                IsLoading = true;
                //call the query again and re populate the data grid with fresh data
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
