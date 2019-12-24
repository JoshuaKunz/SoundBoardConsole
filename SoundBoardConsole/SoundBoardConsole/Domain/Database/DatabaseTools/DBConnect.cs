using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace SoundBoardConsole.Domain.Database.DatabaseTools
{
    public class DBConnect
    {
        private readonly string _connectionString;

        public DBConnect()
        {
            _connectionString = $"Data Source={Environment.CurrentDirectory}/sounds.db;Version=3;New=False;";
            Init();
            Con = new SQLiteConnection(_connectionString);
            Command = new SQLiteCommand(Con);
        }

        public SQLiteCommand Command { get; set; }
        public SQLiteConnection Con { get; set; }

        private void Init()
        {
            if(!File.Exists(Environment.CurrentDirectory + "/sounds.db"))
            {
                SQLiteConnection.CreateFile(Environment.CurrentDirectory + "/sounds.db");

                var sql = "CREATE TABLE AllSounds" +
                    "(id integer unique primary key autoincrement," +
                    "name varchar(20)," +
                    "path varchar(255)," +
                    "keyBinding varchar(10));";
                Con = new SQLiteConnection(_connectionString);
                var command = new SQLiteCommand(sql, Con);
                Con.Open();
                command.ExecuteNonQuery();
                Con.Close();
            }
        }

        public List<Sound> GetSounds()
        {
            var sql = "SELECT name, path, keybinding FROM AllSounds;";
            Con.Open();

            Command.CommandText = sql;

            var reader = Command.ExecuteReader();

            var sounds = new List<Sound>();

            while(reader.Read())
            {
                sounds.Add(new Sound
                {
                    Name = reader.GetString(0),
                    Path = reader.GetString(1),
                    KeyBinding = reader.GetString(2)
                });
            }

            reader.Close();
            Con.Close();
            return sounds;
        }

        public void InsertSound(Sound sound)
        {
            var sql = $"INSERT INTO AllSounds " +
                $"(name, path, keyBinding) " +
                $"VALUES " +
                $"('{sound.Name}','{sound.Path}','{sound.KeyBinding}');";

            Command.CommandText = sql;
            Con.Open();
            Command.ExecuteNonQuery();
            Con.Close();
        }

        public void DeleteSound(string name)
        {
            var sql = $"DELETE FROM AllSounds " +
                $"WHERE name='{name}';";
            Command.CommandText = sql;
            Con.Open();
            Command.ExecuteNonQuery();
            Con.Close();
        }

    }
}
