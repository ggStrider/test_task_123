using System.IO;
using UnityEngine;

namespace Internal.Scripts.Core.Data.Services.Saves
{
    public class SaveService : ISaveService
    {
        private const string FileName = "save.json";
        
        private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

        public void Save(PlayerData from)
        {
            var json = JsonUtility.ToJson(from);
            File.WriteAllText(SavePath, json);
        }

        public void Load(PlayerData to)
        {
            if (!File.Exists(SavePath)) 
                return;
    
            var json = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(json, to);
        }
    }
}