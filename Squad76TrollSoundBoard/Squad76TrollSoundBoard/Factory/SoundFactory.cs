using Squad76TrollSoundBoard.Models;
using Squad76TrollSoundBoard.ViewModels;
using System.Linq;

namespace Squad76TrollSoundBoard.Factory
{
    public class SoundFactory : ISoundFactory
    {
        public SoundGroupViewModel ConvertSoundGroupModel(SoundGroup model)
        {
            return new SoundGroupViewModel
            {
                Sounds = model.Sounds.Select(ConvertSoundModel).ToList(),
                GroupName = model.GroupName
            };
        }

        public SoundViewModel ConvertSoundModel(SoundModel model)
        {
            return new SoundViewModel
            {
                Path = model.Path,
                GroupName = model.GroupName,
                Name = model.Name,
                KeyBinding = model.KeyBinding
            };
        }
    }
}
