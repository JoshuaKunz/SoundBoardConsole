using Squad76TrollSoundBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Squad76TrollSoundBoard.Database
{
    public class DbConnect
    {
        public DbConnect()
        {
            if (!File.Exists(Environment.CurrentDirectory + @"/sounds.db"))
            {
                SQLiteConnection.CreateFile(Environment.CurrentDirectory + @"/sounds.db");
                _ = CreateDatabase();
            }
        }

        public static string ConnectionString = $@"Data Source={Environment.CurrentDirectory}/sounds.db;Version=3;New=False;";

        public SQLiteConnection Connection = new SQLiteConnection(ConnectionString);

        public async Task CreateDatabase()
        {
            var sql = $@"CREATE DATABASE sounds;";
            await NonQuery(sql);

            sql = $@"CREATE TABLE sounds.AllSounds (
                      id int unique primary key,
                      name varchar(25),
                      groupName varchar(25) NOT NULL,
                      keyBinding varchar(25),
                      path varchar(255));";
            await NonQuery(sql);
        }

        public async Task NonQuery(string sql)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }

            var command = new SQLiteCommand(sql, Connection);
            await command.ExecuteNonQueryAsync();
            command.Dispose();

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public async Task<DbDataReader> Query(string sql)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }

            var command = new SQLiteCommand(sql, Connection);
            var reader = await command.ExecuteReaderAsync();
            command.Dispose();

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }

            return reader;
        }

        public async Task<List<SoundModel>> ConvertReaderToSounds(DbDataReader reader)
        {
            var sounds = new List<SoundModel>();

            while (await reader.NextResultAsync())
            {
                var sound = new SoundModel();

                sound.Path = reader.GetString(4);
                sound.KeyBinding = reader.GetString(3);
                sound.GroupName = reader.GetString(2);
                sound.Name = reader.GetString(1);

                sounds.Add(sound);
            }

            reader.Close();
            return sounds;
        }
    }
}
