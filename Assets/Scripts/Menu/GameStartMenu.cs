using System;
using Manager;
using Newtonsoft.Json.Linq;
using UnityEditor.ShaderGraph.Serialization;
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
            JObject json = new JObject() {
                { "hello", "helo" },
            };
            GameManager.Instance.client.login.Send(json);

        }
        
        
    }
}