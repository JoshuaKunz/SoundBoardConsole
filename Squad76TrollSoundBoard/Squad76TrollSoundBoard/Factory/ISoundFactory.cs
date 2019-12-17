using Squad76TrollSoundBoard.Models;
using Squad76TrollSoundBoard.ViewModels;

namespace Squad76TrollSoundBoard.Factory
{
    public interface ISoundFactory
    {
        SoundViewModel ConvertSoundModel(SoundModel model);
        SoundGroupViewModel ConvertSoundGroupModel(SoundGroup model);
    }
}
