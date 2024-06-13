using System;
using System.Collections.Generic;
using Manager.DesignPattern;
using Network;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager: Singleton<GameManager> {

        
        // public EntityManager entity_manager;

        public Dictionary<ManagerType, BaseManager> managers;
        public Dictionary<ManagerType, string> manager_names;
        public Client client;
        private Ref<String> token, check;
        
        public void Login(string name, string passwd) {
            // GameManager.Instance.client.Login("admin", "admin");
            client.Login(name, passwd);
            StartCoroutine(client.login.Receive(10f, token));
        }

        public void CheckUser()
        {
            client.login.Send(new JObject()
            {
                { "token", this.token.Value }
            }
            );

            StartCoroutine(client.login.Receive(10f, check));

        }
        private void Init()
        {
            managers = new Dictionary<ManagerType, BaseManager>();
            manager_names = new Dictionary<ManagerType, string>()
            {
                {ManagerType.EntityManager, "EntityManager"},
                {ManagerType.EventManager, "EventManager"},
                {ManagerType.GameManager, "GameManager"},
                { ManagerType.SoundManager, "SoundManager"},
                {ManagerType.SceneManager, "GameSceneManager"}
            };
            
            // manager add
            managers.Add(ManagerType.EntityManager,
                 this.gameObject.AddComponent(typeof(EntityManager)) as EntityManager); // Entity Manager
            
            managers.Add(ManagerType.SoundManager,
                this.gameObject.AddComponent(typeof(SoundManager)) as SoundManager); // Sound Manager
            managers.Add(ManagerType.SceneManager,
                this.gameObject.AddComponent(typeof(GameSceneManager)) as GameSceneManager); // Game Scene Manager
            

            bool check_error = false;
            // error check
            foreach (ManagerType type in managers.Keys) {
                switch (type) {
                    case ManagerType.EventManager:
                    case ManagerType.EntityManager:
                    case ManagerType.SoundManager:
                    case ManagerType.SceneManager:
                        if (managers[type].Init()) 
                            Debug.LogError($"[{manager_names[type]}] {manager_names[type]} load failed");
                        else
                            Debug.Log($"[{manager_names[type]}] {manager_names[type]} load success");
                        break;
                    default:
                        Debug.LogError($"[GameManager] Manager Load Error!, {manager_names[type]}");
                        check_error = true;
                        break;
                }
            }
            
            if(check_error)
                GameQuit();

            client = new Client();

            check = new Ref<string>("");
            token = new Ref<string>("");
            Debug.Log("[GameManager] Managers Loaded Success");
            
        }
        
        public void Awake() { Init(); }
        
        
        #region ManagerGet

        public EntityManager GetEntityManager() {
            return ((EntityManager) managers[ManagerType.EntityManager]);
        }
        
        public SoundManager GetSoundManager() {
            return ((SoundManager) managers[ManagerType.SoundManager]);
        }

        public GameSceneManager GetSceneManager() {
            return ((GameSceneManager) managers[ManagerType.SceneManager]);
        }
        
        #endregion ManagerGet
        
        public static void GameQuit() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            UnityEditor.EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
        }
    }
}