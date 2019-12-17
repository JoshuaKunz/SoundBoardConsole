using Squad76TrollSoundBoard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squad76TrollSoundBoard.Services
{
    public interface ISoundBoardServices
    {
        Task<List<SoundGroup>> GetSoundGroupByName(string name);
        Task<List<SoundGroup>> GetAllSoundGroups();
        Task<SoundModel> GetSoundById(int id);
        Task<List<SoundModel>> GetAllSounds();
        Task<SoundModel> GetSoundByName(string name);

        Task InsertNewSound(string name, string groupName, string keyBinding, string filePath);
        Task DeleteSoundById(int id);
    }
}
