using System;
using Manager;
using UnityEngine;

namespace Menu
{
    public class GameStartMenu : MonoBehaviour
    {
        private GameStartAnimation animation;
        
        public void Awake()
        {
            animation = this.gameObject.GetComponent<GameStartAnimation>();
            
        }

        public void Start() {
            GameManager.Instance.client.login.Connect();
            animation.Animation();
            Invoke("Send", 5f);
        }

        public void Send()
        {

        }
        
        
    }
}