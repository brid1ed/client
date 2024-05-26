using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Animation.UI;
using Manager;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GameStartAnimation : MonoBehaviour
    {

        [Header("Resource")] 
        [SerializeField] private Image team_logo_image; // snake로 작성할게요
        [SerializeField] private Image game_logo_image;
        [SerializeField] private AudioClip netflix; // snake로 작성할게요
        [Header("Delay")] [SerializeField] private float delay = 0.02f;

        

        private FadeAnimation fade_animation;
        bool error_check = false;

        
        

        private void Awake()
        {
            if (team_logo_image == null) { Debug.LogError("[Game-Start-Menu] \"team_logo_image\" Not Found"); error_check = true; }
            if (game_logo_image == null) { Debug.LogError("[Game-Start-Menu] \"game_logo_image\" Not Found"); error_check = true; }

            if (error_check) GameManager.GameQuit();
            else Debug.Log("[Game-Start-Menu] Game Start");
        }

        void Start()
        {
            
            fade_animation = new FadeAnimation();
            StartCoroutine(fade_animation.FadeBoth(team_logo_image, delay, 0.02f, -0.05f));
            GameManager.Instance.GetSoundManager().JustPlay(netflix);
            StartCoroutine(fade_animation.FadeIn(game_logo_image, delay, 0.01f, -0.05f));
        }
        void Update() {
            
        }



    }
}