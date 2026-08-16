using System.IO;
using UnityEngine;

namespace Game.Persistence
{
    public class PlayerStateManager : MonoBehaviour
    {
        public static PlayerStateManager Instance;
        public PlayerState CurrentState;

        private string savePath => Path.Combine(Application.persistentDataPath, "player_state.json");

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Load();
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(CurrentState);
            File.WriteAllText(savePath, json);
        }

        public void Load()
        {
            if (!File.Exists(savePath))
            {
                CurrentState = new PlayerState();
                Save();
                return;
            }

            var json = File.ReadAllText(savePath);
            CurrentState = JsonUtility.FromJson<PlayerState>(json);

            if(CurrentState == null)
            {
                CurrentState = new PlayerState();
                Save();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Save();
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}
