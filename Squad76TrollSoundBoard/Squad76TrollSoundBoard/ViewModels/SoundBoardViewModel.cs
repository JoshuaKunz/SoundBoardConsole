using Squad76TrollSoundBoard.Commands;
using Squad76TrollSoundBoard.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;
using Squad76TrollSoundBoard.Ninject;
using Squad76TrollSoundBoard.Database;
using System.Windows;
using Squad76TrollSoundBoard.Audio;
using Squad76TrollSoundBoard.Factory;
using System.Linq;

namespace Squad76TrollSoundBoard.ViewModels
{
    public class SoundBoardViewModel : INotifyPropertyChanged
    {
        private readonly StandardKernel kernel;
        private readonly ISoundBoardServices _services;
        private readonly ISoundFactory _factory;
        private readonly DbConnect _db;
        private readonly AudioPlayer player;

        public SoundBoardViewModel()
        {
            kernel = new StandardKernel(new SoundBoardNinjectModule());
            _db = new DbConnect();
            _services = kernel?.Get<SoundBoardServices>();
            _factory = kernel?.Get<SoundFactory>();
            player = new AudioPlayer();
            _ = LoadData();
            AddNewSoundGroupCommand = new RelayCommand(CreateNewSoundGroup);
        }

        private SoundGroupViewModel _selectedSoundGroup;
        public SoundGroupViewModel SelectedSoundGroup
        {
            get => _selectedSoundGroup;
            set
            {
                if (value == null)
                    return;

                _selectedSoundGroup = value;
                OnPropertyChanged("SelectedSoundGroup");
            }
        }

        public List<SoundViewModel> Sounds => SelectedSoundGroup?.Sounds;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsLoading { get; set; }

        public void CreateNewSoundGroup()
        {
            var folder = Environment.CurrentDirectory + "/../../../../Sounds";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public async Task LoadData()
        {
            await Task.Run(async () =>
            {
                IsLoading = true;
                var folder = Environment.CurrentDirectory + "/../../../../Sounds";

                var soundModels = await _services.GetAllSounds();

                //loop throught the files in the sounds folder.
                foreach (var sound in Directory.GetFiles(folder))
                {
                    //the database does not contain it and should be entered into the database
                    //await _services.InsertNewSound(sound.Split('\\').LastOrDefault(), "Test", "", sound);
                    MessageBox.Show(sound);

                }
            });
        }

        //add a method and command to work with the button in the menu to add a new sound group
        public ICommand AddNewSoundGroupCommand { get; set; }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
