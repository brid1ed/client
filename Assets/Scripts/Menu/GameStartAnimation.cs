using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Animation.UI;
using Manager;
using Network;


namespace Menu
{
    public class GameStartAnimation : MonoBehaviour
    {

        [Header("Resource")] 
        [SerializeField] private Sprite team_gray;
        [SerializeField] private Sprite team_color;
        [SerializeField] private Image team_logo_image; // snake로 작성할게요
        [SerializeField] private Image game_logo_image;
        [SerializeField] private Image game_title_image;
        [SerializeField] private AudioClip team_sfx; // snake로 작성할게요
        [SerializeField] private AudioClip start_sound; // snake로 작성할게요
        [SerializeField] private GameObject Buttons;

        [Header("Delay")] [SerializeField] private float delay = 0.02f;

        

        private FadeAnimation fade_animation;
        bool error_check = false;

        
        

        private void Awake()
        {
            team_logo_image.sprite = team_gray;

            if (team_logo_image == null) { Debug.LogError("[Game-Start-Menu] \"team_logo_image\" Not Found"); error_check = true; }
            if (game_logo_image == null) { Debug.LogError("[Game-Start-Menu] \"game_logo_image\" Not Found"); error_check = true; }

            if (error_check) GameManager.GameQuit();
            else Debug.Log("[Game-Start-Menu] Game Start");
        }


        public void Animation() {
            StartCoroutine(anim());
        }

        IEnumerator anim() {
            fade_animation = new FadeAnimation();
            StartCoroutine(fade_animation.FadeBoth(team_logo_image, delay, 0.012f, -0.05f));
            yield return new WaitForSeconds(1.3f);
            
            GameManager.Instance.GetSoundManager().NextSoundAdd(0, new SoundClip(team_sfx, 0.6f));

            for (int i = 0; i < 20; i++) {
                team_logo_image.transform.localPosition = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-2f, 2f));
                yield return new WaitForSeconds(0.05f);
            }

            EffectManager.Instance.StartGlitch(0.2f);
            yield return new WaitForSeconds(0.1f);

            team_logo_image.sprite = team_color;

            yield return new WaitForSeconds(2f);

            StartCoroutine(fade_animation.FadeOut(team_logo_image, delay, 0.1f, -0.05f));
            StartCoroutine(fade_animation.FadeIn(game_logo_image, delay, 0.01f, -0.05f));
            StartCoroutine(fade_animation.FadeIn(game_title_image, delay, 0.01f, -0.1f));
            StartCoroutine(fade_animation.FadeInChildren(Buttons, delay, 0.01f, -0.1f));

            yield return new WaitForSeconds(1.3f);

            GameManager.Instance.GetSoundManager().NextSoundAdd(0, new SoundClip(start_sound));
        }


        public void OnStartClick() {
            GameManager.Instance.GetSceneManager().LoadScene("Test");
        }


        


    }
}