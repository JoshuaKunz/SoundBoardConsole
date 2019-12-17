using Squad76TrollSoundBoard.Database;
using Squad76TrollSoundBoard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squad76TrollSoundBoard.Services
{
    public class SoundBoardServices : ISoundBoardServices
    {
        private readonly DbConnect _dbConnect;

        public SoundBoardServices()
        {
            _dbConnect = new DbConnect();
        }

        public Task DeleteSoundById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SoundGroup>> GetAllSoundGroups()
        {
            var sql = "SELECT groupName FROM sounds.AllSounds;";
            var reader = await _dbConnect.Query(sql);

            var groupNames = new List<string>();

            while (await reader.NextResultAsync())
            {
                groupNames.Add(reader.GetString(0));
            }

            reader.Close();

            var soundGroups = new List<SoundGroup>();
            foreach (var group in groupNames)
            {
                sql = $@"SELECT name, groupName, keyBinding, path
                         FROM sounds.AllSounds
                         WHERE groupName = '{group}';";
                reader = await _dbConnect.Query(sql);

                var soundGroup = new SoundGroup();
                soundGroup.GroupName = group;

                var sounds = new List<SoundModel>();

                while (await reader.NextResultAsync())
                {
                    sounds.Add(new SoundModel
                    {
                        Name = reader.GetString(0),
                        GroupName = reader.GetString(1),
                        KeyBinding = reader.GetString(2),
                        Path = reader.GetString(3)
                    });
                }
                reader.Close();
                soundGroup.Sounds = sounds;
                soundGroups.Add(soundGroup);
            }
            return soundGroups;
        }

        public async Task<List<SoundModel>> GetAllSounds()
        {
            var sql = $@"SELECT name, groupName, keyBinding, path
                         FROM sounds.AllSounds;";
            var reader = await _dbConnect.Query(sql);

            var sounds = new List<SoundModel>();

            while (await reader.NextResultAsync())
            {
                sounds.Add(new SoundModel
                {
                    Name = reader.GetString(0),
                    GroupName = reader.GetString(1),
                    KeyBinding = reader.GetString(2),
                    Path = reader.GetString(3)
                });
            }

            return sounds;
        }

        public async Task<SoundModel> GetSoundById(int id)
        {
            var sql = $@"SELECT name, groupName, keyBinding, path
                            FROM sounds.AllSounds;";
            var reader = await _dbConnect.Query(sql);

            var sound = new SoundModel();

            while(await reader.NextResultAsync())
            {
                sound = new SoundModel 
                { 
                    Name = reader.GetString(0),
                    GroupName = reader.GetString(1), 
                    KeyBinding = reader.GetString(2),
                    Path = reader.GetString(3)
                };
            }
            return sound;
        }

        public Task<SoundModel> GetSoundByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SoundGroup>> GetSoundGroupByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task InsertNewSound(string name, string groupName, string keyBinding, string filePath)
        {
            var sql = $@"INSERT INTO sounds.AllSounds
                         (name, groupName, keyBinding, path) VALUES
                         ('{name}', '{groupName}', '{keyBinding}', '{filePath}');";

            await _dbConnect.NonQuery(sql);
        }
    }
}
