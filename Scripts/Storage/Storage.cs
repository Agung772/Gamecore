using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

namespace Core
{
    public class Storage : GlobalBehaviour
    {
        private const string FILE_NAME = "Save.txt";
        private string PathFile => Path.Combine(Application.persistentDataPath, FILE_NAME);
        
        private readonly JsonSerializerSettings jsonSettings = new() { TypeNameHandling = TypeNameHandling.Auto };
        private Dictionary<Type, BaseStorage> storages = new();
        public T Get<T>() where T : BaseStorage, new() => storages[typeof(T)] as T;

        public override void Initialize()
        {
            Create();
            Load();
            Application.quitting += Save;
        }

        private void Create()
        {
            storages = InstanceUtility.Create<BaseStorage>();
            foreach (var _storage in storages.Values)
            {
                _storage.OnCreate();
            }
        }
        private void Load()
        {
            if (File.Exists(PathFile))
            {
                var _json = File.ReadAllBytes(PathFile);
                var _decrypt = Encryption.Decrypt(_json);
                var _temp = JsonConvert.DeserializeObject<Dictionary<Type, BaseStorage>>(_decrypt, jsonSettings);
                foreach (var _key in storages.Keys.ToList())
                {
                    if (_temp.TryGetValue(_key, out var _storage))
                    {
                        storages[_key] = _storage;
                        storages[_key].OnLoad();
                    }
                }
            }
            else
            {
                foreach (var _key in storages.Keys.ToList())
                {
                    storages[_key].OnLoad();
                }
            }
        }
        public void Save()
        {
            var _json = JsonConvert.SerializeObject(storages, jsonSettings);
            var _encrypt = Encryption.Encrypt(_json);
            File.WriteAllBytes(PathFile, _encrypt);

            #if UNITY_EDITOR
            var _pathEditorSave = Path.Combine(Application.persistentDataPath, PathFile.Replace(".", "Editor."));
            File.WriteAllText(_pathEditorSave, _json);
            #endif

            Debug.Log($"Save Game Data \n" +
                      $"Path : {PathFile}");
        }
    }
}

