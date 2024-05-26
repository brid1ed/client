using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.DesignPattern {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance != null)
            {
                if (_instance != this) Destroy(this.gameObject);
                return;
            }


            _instance = this.gameObject.transform.GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    
}