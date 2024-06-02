using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Animation.UI;
using Manager;


namespace Menu
{
    public class GameStartAnimation : MonoBehaviour
    {

        [Header("Resource")] 
        [SerializeField] private Image team_logo_image; // snake로 작성할게요
        [SerializeField] private Image game_logo_image;
        [SerializeField] private Image game_title_image;
        [SerializeField] private AudioClip netflix; // snake로 작성할게요
        [SerializeField] private AudioClip start_sound; // snake로 작성할게요
        [SerializeField] private GameObject Buttons;

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


        public void Animation() {
            fade_animation = new FadeAnimation();
            GameManager.Instance.GetSoundManager().NextSoundAdd(0, new SoundClip(netflix));
            StartCoroutine(fade_animation.FadeBoth(team_logo_image, delay, 0.02f, -0.05f));
            StartCoroutine(fade_animation.FadeIn(game_logo_image, delay, 0.01f, -0.05f));
            GameManager.Instance.GetSoundManager().NextSoundAdd(0, new SoundClip(start_sound));
            StartCoroutine(fade_animation.FadeIn(game_title_image, delay, 0.005f, -0.1f));
            StartCoroutine(fade_animation.FadeInChildren(Buttons, delay, 0.03f, -0.1f));
        }


        public void OnStartClick() {
            GameManager.Instance.GetSceneManager().LoadScene("Test");
        }


        public void Login()
        {
            GameManager.Instance.client.Login("admin", "admin");
            StartCoroutine(GameManager.Instance.client.login.Receive());
        }


    }
}