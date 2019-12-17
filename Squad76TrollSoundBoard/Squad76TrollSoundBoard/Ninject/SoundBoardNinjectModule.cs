using Ninject.Modules;
using Squad76TrollSoundBoard.Factory;
using Squad76TrollSoundBoard.Services;
using Squad76TrollSoundBoard.ViewModels;

namespace Squad76TrollSoundBoard.Ninject
{
    public class SoundBoardNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel?.Bind<ISoundBoardServices>().To<SoundBoardServices>();
            Kernel?.Bind<SoundBoardViewModel>().ToSelf();
            Kernel?.Bind<ISoundFactory>().To<SoundFactory>();
        }
    }
}
