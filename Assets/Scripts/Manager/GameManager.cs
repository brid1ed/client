using System;
using System.Collections.Generic;
using Manager.DesignPattern;
using UnityEngine;

namespace Manager
{
    public class GameManager: Singleton<GameManager> {

        
        // public EntityManager entity_manager;

        public Dictionary<ManagerType, BaseManager> managers;
        public Dictionary<ManagerType, string> manager_names;



        private void Init()
        {
            managers = new Dictionary<ManagerType, BaseManager>();
            manager_names = new Dictionary<ManagerType, string>()
            {
                {ManagerType.EntityManager, "EntityManager"},
                {ManagerType.EventManager, "EventManager"},
                {ManagerType.GameManager, "GameManager"},
            };
            
            // manager add
            managers.Add(ManagerType.EntityManager,
                 this.gameObject.AddComponent(typeof(EntityManager)) as EntityManager); // Entity Manager


            bool check_error = false;
            // error check
            foreach (ManagerType type in managers.Keys) {
                
                switch (type) {
                    case ManagerType.EventManager:
                    case ManagerType.EntityManager:
                        break;
                    default:
                        Debug.LogError($"[GameManager] Manager Load Error!, {manager_names[type]}");
                        check_error = true;
                        break;
                }
            }
            if(check_error)
                GameQuit();
            
            Debug.Log("[GameManager] Managers Loaded Success");
            
        }
        
        public void Awake() { Init(); }
        
        
        #region ManagerGet

        public EntityManager GetEntityManager() {
            return ((EntityManager) managers[ManagerType.EntityManager]);
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