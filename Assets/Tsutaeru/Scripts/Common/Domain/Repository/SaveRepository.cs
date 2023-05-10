using Tsutaeru.Common.Data.DataStore;
using UnityEngine;

namespace Tsutaeru.Common.Domain.Repository
{
    public sealed class SaveRepository
    {
        public SaveData Load()
        {
            var data = ES3.Load(SaveKeyConfig.ES3_KEY, defaultValue: "");

            if (string.IsNullOrEmpty(data))
            {
                return Create();
            }

            return JsonUtility.FromJson<SaveData>(data);
        }

        private SaveData Create()
        {
            var newData = new SaveData
            {
                uid = "",
                bgmVolume = SoundConfig.INIT_VOLUME,
                seVolume = SoundConfig.INIT_VOLUME,
            };
            Save(newData);

            return newData;
        }

        public void SaveUid(string uid)
        {
            var loadData = Load();
            loadData.uid = uid;
            Save(loadData);
        }

        public void SaveBgm(float bgmVolume)
        {
            var loadData = Load();
            loadData.bgmVolume = bgmVolume;
            Save(loadData);
        }

        public void SaveSe(float seVolume)
        {
            var loadData = Load();
            loadData.seVolume = seVolume;
            Save(loadData);
        }

        public void Delete()
        {
            ES3.DeleteKey(SaveKeyConfig.ES3_KEY);
        }

        private void Save(SaveData saveData)
        {
            var data = JsonUtility.ToJson(saveData);
            ES3.Save(SaveKeyConfig.ES3_KEY, data);
        }
    }
}